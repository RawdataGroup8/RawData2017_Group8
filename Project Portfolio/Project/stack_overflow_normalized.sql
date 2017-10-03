DROP DATABASE if exists stack_overflow_normalized;
CREATE DATABASE stack_overflow_normalized;
USE stack_overflow_normalized;

-- ---------------- DATABASE CREATION AND DATA INSERTION -------------------

-- user (user_id(PK), user_name, user_creation_date, user_location, user_age) 
CREATE TABLE user (
    user_id INT UNSIGNED PRIMARY KEY,
    user_name VARCHAR(30) NOT NULL,
    user_creation_date DATETIME,
    user_location VARCHAR(200),
    user_age INT UNSIGNED
);

insert into user (user_id, user_name, user_creation_date, user_location, user_age)
select distinct owneruserid, owneruserdisplayname, ownerusercreationdate, owneruserlocation, owneruserage
from stackoverflow_sample_universal.posts
union
select distinct userid, userdisplayname, usercreationdate, userlocation, userage
from stackoverflow_sample_universal.comments;

-- insert ignore into user (user_id, user_name, user_creation_date, user_location, user_age)
-- select userid, userdisplayname, usercreationdate, userlocation, userage
-- from stackoverflow_sample_universal.comments;

-- post(post_id(PK), creation_date, score, body, title, owner_user_id(FK), type_id) /* type id somewhat redundant */
CREATE TABLE post (
    post_id INT UNSIGNED PRIMARY KEY,
    creation_date DATETIME,
    score BIGINT,
    body TEXT,
    title VARCHAR(300),
    owner_user_id INT UNSIGNED NOT NULL REFERENCES user (user_id),
    type_id INT UNSIGNED,
    FULLTEXT (title,body)
);

insert into post (post_id, creation_date, score, body, title, owner_user_id, type_id)
select distinct id, creationdate, score, body, title, owneruserid, posttypeid
from stackoverflow_sample_universal.posts;

-- question(post_id(PK,FK), accepted_answer_id(FK), closed_date, tags) /* no need for tags here right? */ 
CREATE TABLE question (
    post_id INT PRIMARY KEY REFERENCES post (post_id),
    accepted_answer_id INT REFERENCES post (post_id),
    closed_date DATETIME
);

insert into question (post_id, accepted_answer_id, closed_date) 
select distinct id, acceptedanswerid, closeddate
from stackoverflow_sample_universal.posts as p where p.posttypeid = 1;


-- answer(parent_id, post_id(PK,FK))
CREATE TABLE answer (
    post_id INT PRIMARY KEY REFERENCES post (post_id),
    parent_id INT
);

insert into answer (post_id, parent_id) 
select distinct id, parentid
from stackoverflow_sample_universal.posts as p where p.posttypeid = 2;

-- comment(comment_id(PK), comment_score, comment_text, comment_create_date, user_id(FK), post_id(FK))
CREATE TABLE comment (
    comment_id INT PRIMARY KEY,
    comment_score INT,
    comment_text MEDIUMTEXT,
    comment_create_date DATETIME,
    user_id INT REFERENCES user(user_id),
    post_id INT references post(post_id)    
);
insert into comment (comment_id, comment_score, comment_text, comment_create_date, user_id, post_id) 
select commentid, commentscore, commenttext, commentcreatedate, userid, postid
from stackoverflow_sample_universal.comments;

-- post_tags((post_id(FK), tag)(PK))//tag_id
-- drop table if exists post_tags;
CREATE TABLE post_tags (
    post_id INT REFERENCES post (post_id),
    tag_name VARCHAR(50),
    primary key(post_id, tag_name)
);

-- tags(TAG_ID, tag)  <split tags on '::'> 
/*drop table if exists tags;
CREATE TABLE tags (
    tag_id INT AUTO_INCREMENT PRIMARY KEY,
    tag_name VARCHAR(50)
);*/

-- string_at_delimited_pos(str, delim, pos) /* returns the value at n'th position eg. string_at_delimited_pos("a::b::c::d", "::", 3) returns "c" */ 
-- drop function if exists string_at_delimited_pos;
 CREATE FUNCTION string_at_delimited_pos(str VARCHAR(255), delim VARCHAR(12), pos INT)
 RETURNS VARCHAR(255)
 RETURN REPLACE(SUBSTRING(SUBSTRING_INDEX(str, delim, pos),
        LENGTH(SUBSTRING_INDEX(str, delim, pos -1)) + 1),
        delim, '');
-- select string_at_delimited_pos("qwer::rtyu::khhj","::",2);

-- num_values_in_delimited_str(str) /* Returns the number of values such that a::b::c returns 3*/
DELIMITER //
CREATE FUNCTION num_values_in_delimited_str ( tag varchar(200) )
RETURNS INT
BEGIN
   RETURN ROUND((LENGTH(tag) - LENGTH(REPLACE(tag, '::', ''))) / LENGTH('::')) + 1; -- number of :: in string + 1 (number of tags);
END; //
DELIMITER ;

/* This procedure creates a cursor containing id, tag from the original 'posts' table,
 iterates over that splitting up each tag inside the original 'tags' string, and inserts them
 into the post_tag table */
-- drop procedure if exists split_insert_into_tags;
delimiter //
create procedure split_insert_into_tags()
begin
	declare done int default false;
	declare tag varchar(90);
    declare post_id int;
    declare i_max int;
	declare i int;
    
	declare tags_cur cursor for select id, tags from stackoverflow_sample_universal.posts;
	declare continue handler for not found set done = true;
	
    open tags_cur;	
		read_loop: loop
			fetch tags_cur into post_id, tag;
			set i_max = num_values_in_delimited_str(tag);-- ROUND((LENGTH(tag) - LENGTH(REPLACE(tag, '::', ''))) / LENGTH('::')) + 1; -- number of :: in string + 1 (number of tags)
			set i = 1;
			if done then
				leave read_loop;
			end if;
			start transaction;
				while i <= i_max do
					insert ignore into post_tags(post_id, tag_name) values (post_id, (string_at_delimited_pos(tag, "::", i)));
					set i=i+1;
				end while;
			commit;		
		end loop;
	close tags_cur;
end;//
delimiter ;

call split_insert_into_tags();

-- linked_posts(LINK_POST_ID, POST_ID)
CREATE TABLE linked_posts (
    post_id INT REFERENCES post (post_id),
    link_post_id INT REFERENCES post (post_id),
    PRIMARY KEY (link_post_id , post_id)
);

insert into linked_posts (post_id, link_post_id) 
select id, linkpostid
from stackoverflow_sample_universal.posts where linkpostid > "";

-- history((USER_ID, DATE_TIME_ADDED)(PK), post_id(FK)); /*Index by users for search*/
CREATE TABLE history (
    user_id INT REFERENCES user(user_id),
    datetime_added DATETIME,
    link_post_id INT REFERENCES post (post_id),
    PRIMARY KEY (user_id, datetime_added)
);
-- marking(USER_ID, POST_ID, date_time_added, folder_tag); 
CREATE TABLE marking (
    user_id INT REFERENCES user(user_id),
    post_id INT REFERENCES post(post_id),
    datetime_added DATETIME,
    folder_tag varchar(200),
    PRIMARY KEY (user_id, datetime_added)
);
-- //post_type(type_id, type); /* somewhat redundant. a post with a parentid is an answer, posts without are questions */ /* could also remove and store type directly in the post table*/

-- ---------------- FUNCTIONALITY / API -------------------

-- search_questions_by_tag(tag, lim) /* Basic tag search query. Returns relevant data about questions that contains the <tag>, ordered by score, limited to <lim> */
-- drop procedure if exists search_questions_by_tag;
DELIMITER //
CREATE PROCEDURE search_questions_by_tag (IN tag varchar(200), lim int)
BEGIN
	select post.post_id, title, body, score, creation_date, closed_date from post, question, post_tags
    where post.post_id = question.post_id and post.post_id = post_tags.post_id and post_tags.tag_name = tag
    order by post.score desc limit lim;
END //
DELIMITER ;
-- call search_questions_by_tag("c#", 50);

-- retrieve_answers(question_id) /* Retrieves the answers to a given question */
-- drop procedure if exists retrieve_answers;
DELIMITER //
CREATE PROCEDURE retrieve_answers (IN question_id int, lim int)
BEGIN
	select */*post.post_id, title, body, score, creation_date*/ from post, answer
    where post.post_id = answer.post_id
    order by post.score desc limit lim;
END //
DELIMITER ;
-- call retrieve_answers(9033, 50);

-- fulltext_search(search_str) /* Procedure makes use of mysql's built in full text search */
-- drop procedure if exists fulltext_search;
DELIMITER //
CREATE PROCEDURE fulltext_search (in search_str varchar(400))
BEGIN
	SELECT post_id, title, body, MATCH (title,body) AGAINST
	(search_str IN NATURAL LANGUAGE MODE)  AS score
    FROM post WHERE post.type_id = 1 and MATCH (title,body) AGAINST
    (search_str IN NATURAL LANGUAGE MODE);
END //
DELIMITER ;
-- call fulltext_search('javascript tutorial');
-- call fulltext_search('machine learning');
-- call fulltext_search('how to python good');
-- call fulltext_search('Hi help me be the bestest at searching thank you');

/*
SELECT * FROM post
WHERE MATCH (title , body) AGAINST ('javascript ' IN NATURAL LANGUAGE MODE);

SELECT post_id, title, body, MATCH (title,body) 
AGAINST ('coffee' IN NATURAL LANGUAGE MODE) AS score
FROM post order by score desc;
*/
-- Search by %word% in title
-- Search by tag in body
-- 