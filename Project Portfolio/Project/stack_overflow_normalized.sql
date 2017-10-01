DROP DATABASE if exists stack_overflow_normalized;
CREATE DATABASE stack_overflow_normalized;
USE stack_overflow_normalized;

-- user (user_id(PK), user_name, user_creation_date, user_location, user_age) 
CREATE TABLE user (
    user_id INT UNSIGNED PRIMARY KEY,
    user_name VARCHAR(30) NOT NULL,
    user_creation_date DATE,
    user_location VARCHAR(50),
    user_age INT UNSIGNED
);

insert ignore into user (user_id, user_name, user_creation_date, user_location, user_age)
select owneruserid, owneruserdisplayname, ownerusercreationdate, owneruserlocation, owneruserage
from stackoverflow_sample_universal.posts;

insert ignore into user (user_id, user_name, user_creation_date, user_location, user_age)
select userid, userdisplayname, usercreationdate, userlocation, userage
from stackoverflow_sample_universal.comments;

-- post(post_id(PK), creation_date, score, body, title, owner_user_id(FK), type_id) /* type id somewhat redundant */
CREATE TABLE post (
    post_id INT UNSIGNED PRIMARY KEY,
    creation_date DATETIME,
    score BIGINT,
    body MEDIUMTEXT,
    title VARCHAR(90),
    owner_user_id INT UNSIGNED NOT NULL REFERENCES user (user_id),
    type_id INT UNSIGNED
);

insert ignore into post (post_id, creation_date, score, body, title, owner_user_id, type_id)
select id, creationdate, score, body, title, owneruserid, posttypeid
from stackoverflow_sample_universal.posts;

-- question(post_id(PK,FK), accepted_answer_id(FK), closed_date, tags) /* no need for tags here right? */ 
CREATE TABLE question (
    post_id INT PRIMARY KEY REFERENCES post (post_id),
    accepted_answer_id INT REFERENCES post (post_id),
    closed_date DATETIME
);

insert ignore into question (post_id, accepted_answer_id, closed_date) 
select id, acceptedanswerid, closeddate
from stackoverflow_sample_universal.posts as p where p.posttypeid = 1;


-- answer(parent_id, post_id(PK,FK))
CREATE TABLE answer (
    post_id INT PRIMARY KEY REFERENCES post (post_id),
    parent_id INT
);

insert ignore into answer (post_id, parent_id) 
select id, parentid
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
drop table if exists post_tags;
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

-- returns the value at n'th position eg. string_at_delimited_pos("a::b::c::d", "::", 3) returns "c" 
 drop function if exists string_at_delimited_pos;
 CREATE FUNCTION string_at_delimited_pos(str VARCHAR(255), delim VARCHAR(12), pos INT)
 RETURNS VARCHAR(255)
 RETURN REPLACE(SUBSTRING(SUBSTRING_INDEX(str, delim, pos),
        LENGTH(SUBSTRING_INDEX(str, delim, pos -1)) + 1),
        delim, '');
-- select string_at_delimited_pos("qwer::rtyu::khhj","::",2);

drop procedure if exists split_insert_into_tags;
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
			set i_max = ROUND((LENGTH(tag) - LENGTH(REPLACE(tag, '::', ''))) / LENGTH('::')) + 1; -- number of :: in string + 1 (number of tags)
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

-- //post_type(type_id, type); /* somewhat redundant. a post with a parentid is an answer, posts without are questions */ /* could also remove and store type directly in the post table*/

-- history((USER_ID, DATE_TIME_ADDED)(PK), post_id(FK)); /*Index by users for search*/

-- marking(USER_ID, POST_ID, date_time_added, folder_tag); 

-- linked_posts(LINK_POST_ID, POST_ID)