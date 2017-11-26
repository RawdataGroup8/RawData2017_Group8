
-- All the following procedures use the same (rather extensive) method to allow comma separated strings with any number of values as input.

/* B.1 This procedure retrieves posts that simply match words in the query*/
drop procedure if exists simplematch;
delimiter //
create procedure simplematch (in _wlist varchar(5000))
begin 
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
    DECLARE result TEXT DEFAULT NULL;
    DECLARE firstIter BOOL DEFAULT TRUE;
    
    set result = 'select words.id from words.words, (';
    iterator:
	LOOP
		IF LENGTH(TRIM(_wlist)) = 0 OR _wlist IS NULL THEN
			LEAVE iterator;
		END IF;
        
        /* Dont add ' union all ' on the first iteration */
		IF firstIter = false THEN
			set result = CONCAT(result, ' union all ');
		END IF;        
		set firstIter = false;
        
        /*get everything before first occurence of ','*/
        SET _next = SUBSTRING_INDEX(_wlist,',',1); 
        /*store length of _next'*/
        SET _nextlen = LENGTH(_next); 
        /*add a line to the sql query*/
        SET result = CONCAT(result, 'select distinct id from words.words where word = ', "'",trim(_next),"'");
        /*remove the processed word from the input string*/
        SET _wlist = INSERT(_wlist,1,_nextlen + 1,'');
    END LOOP;
    
    /*close the query string*/
	set @res = concat(result, ') t where t.id=words.id group by t.id limit 15'); 
    
    /*prepare and execute the string with the sql query*/
    PREPARE stmt FROM @res;
    execute stmt;
end//
delimiter ;
call simplematch('mysql, procedures');

/* B.2 This procedure ranks posts based on a simple count of matches */
drop procedure if exists bestmatch;
delimiter //
create procedure bestmatch (in _wlist varchar(5000))
begin 
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
    DECLARE result TEXT DEFAULT NULL;
    DECLARE firstIter BOOL DEFAULT TRUE;
    
    set result = 'select wi.id, sum(score) rank, word from mwib, (';
    iterator:
	LOOP
		IF LENGTH(TRIM(_wlist)) = 0 OR _wlist IS NULL THEN
			LEAVE iterator;
		END IF;
        
        /* Dont add ' union all ' on the first iteration */
		IF firstIter = false THEN
			set result = CONCAT(result, ' union all ');
		END IF;        
		set firstIter = false;
        
        /*get everything before first occurence of ','*/
        SET _next = SUBSTRING_INDEX(_wlist,',',1); 
        /*store length of _next'*/
        SET _nextlen = LENGTH(_next); 
        /*add a line to the sql query*/
        SET result = CONCAT(result, 'select distinct id, 1 score from mwib where word = ', "'",trim(_next),"'");
        /*remove the processed word from the input string*/
        SET _wlist = INSERT(_wlist,1,_nextlen + 1,'');
    END LOOP;
    
    /*close the query string*/
	set @res = concat(result, ') t where t.id=wi.id group by t.id order by rank desc limit 15'); 
    
    /*prepare and execute the string with the sql query*/
    PREPARE stmt FROM @res;
    execute stmt;
end//
delimiter ;
call bestmatch('mysql, procedures');

/*B.3 create word index with tf idf columns*/
drop table if exists ndt;
create table ndt as 
select id, word, count(*) as ndt_ FROM words.words where word regexp '^[A-Za-z][A-Za-z]{1,}$' and word not in (select * from stopwords) group by id, word;
create index ndtIndex on ndt(id, word);

drop table if exists nd;
create table nd as 
select distinct id, count(word) as nd_ FROM words.words where word regexp '^[A-Za-z][A-Za-z]{1,}$' and word not in (select * from stopwords) group by id;
create index ndIndex on nd(id);

drop table if exists nt;
create table nt as 
select word, count(distinct id) as nt_ from words.words where word regexp '^[A-Za-z][A-Za-z]{1,}$' and word not in (select * from stopwords) group by word;
create index ntIndex on nt(word);

drop table if exists wi;
create table wi as 
select id, word, /*ndt_, nd_, nt_,*/ log2(1+(ndt_/nd_)) as tf, (1.0/nt_) as idf, log2(1+(ndt_/nd_))/nt_ as tf_idf 
from words.words natural join ndt natural join nd natural join nt;
create index wiIndex on wi(id, word);

-- clean up
drop table if exists ndt;
drop table if exists nd;
drop table if exists nt;

/* B.4 TF-IDF based ranked posts*/
drop procedure if exists ranked_post_search;
delimiter //
create procedure ranked_post_search (in _wlist varchar(5000))
begin 
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
    DECLARE result TEXT DEFAULT NULL;
    DECLARE firstIter BOOL DEFAULT TRUE;
    
    set result = 'select wi.id, sum(score) as rank from wi, (';
    iterator:
	LOOP
		IF LENGTH(TRIM(_wlist)) = 0 OR _wlist IS NULL THEN
			LEAVE iterator;
		END IF;
        
        /* Dont add ' union all ' on the first iteration */
		IF firstIter = false THEN
			set result = CONCAT(result, ' union all ');
		END IF;        
		set firstIter = false;
        
        /*get everything before first occurence of ','*/
        SET _next = SUBSTRING_INDEX(_wlist,',',1); 
        /*store length of _next'*/
        SET _nextlen = LENGTH(_next); 
        /*add a line to the sql query*/
        SET result = CONCAT(result, 'select distinct id, tf_idf as score from wi where word = ', "'",trim(_next),"'");
        /*remove the processed word from the input string*/
        SET _wlist = INSERT(_wlist,1,_nextlen + 1,'');
    END LOOP;
    
    /*close the query string*/
	set @res = concat(result, ') t where t.id=wi.id group by t.id order by rank desc limit 15'); 
    
    /*prepare and execute the string with the sql query*/
    PREPARE stmt FROM @res;
    execute stmt;
end//
delimiter ;
call ranked_post_search('mysql, replication, java, c');

 -- B.5 Solution for B-5 it can get any size of Query and retuns frequency weigtedwords------ 
drop procedure if exists frequency_weighted;
delimiter //
create procedure frequency_weighted (in _wlist varchar(5000))
begin 
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
    DECLARE result TEXT DEFAULT NULL;
    DECLARE firstIter BOOL DEFAULT TRUE;
    
    -- select word, sum(rank) r from wi, (select id, sum(score) rank 
    -- from(select distinct id, 1 score from wi where word = '
    set result = 'select word, sum(rank) r from wi, (select id, sum(score) rank from(';
    iterator:
	LOOP
		IF LENGTH(TRIM(_wlist)) = 0 OR _wlist IS NULL THEN
			LEAVE iterator;
		END IF;
        
        /* Dont add ' union all ' on the first iteration */
		IF firstIter = false THEN
			set result = CONCAT(result, ' union all ');
		END IF;        
		set firstIter = false;
        
        /*get everything before first occurence of ','*/
        SET _next = SUBSTRING_INDEX(_wlist,',',1); 
        /*store length of _next'*/
        SET _nextlen = LENGTH(_next); 
        /*add a line to the sql query*/
        SET result = CONCAT(result, 'select distinct id, 1 score from wi where word = ', "'",trim(_next),"'");
        /*remove the processed word from the input string*/
        SET _wlist = INSERT(_wlist,1,_nextlen + 1,'');
    END LOOP;
    
    /*close the query string*/
	set @res = concat(result, ') t1 group by id) t2 where wi.id=t2.id and word not in (select * from stopwords) group by word order by r desc limit 10'); 
    
    /*prepare and execute the string with the sql query*/
    PREPARE stmt FROM @res;
    execute stmt;
end//
delimiter ;

call frequency_weighted('mysql, procedures, java, javascript');

/* Extra B.6. This procedure is almost the same as ranked_post_search, but just before prep/execute the required sql to retrieve ranked words is added*/
drop procedure if exists ranked_words;
delimiter //
create procedure ranked_words (in _wlist varchar(5000))
begin 
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
    DECLARE result TEXT DEFAULT NULL;
    DECLARE firstIter BOOL DEFAULT TRUE;
    
    set result = 'select wi.id, sum(score) from wi, (';
    iterator:
	LOOP
		IF LENGTH(TRIM(_wlist)) = 0 OR _wlist IS NULL THEN
			LEAVE iterator;
		END IF;
        
        /* Dont add ' union all ' on the first iteration */
		IF firstIter = false THEN
			set result = CONCAT(result, ' union all ');
		END IF;        
		set firstIter = false;
        
        /*get everything before first occurence of ','*/
        SET _next = SUBSTRING_INDEX(_wlist,',',1); 
        /*store length of _next'*/
        SET _nextlen = LENGTH(_next); 
        /*add a line to the sql query*/
        SET result = CONCAT(result, 'select distinct id, tf*idf as score from wi where word = ', "'",trim(_next),"'");
        /*remove the processed word from the input string*/
        SET _wlist = INSERT(_wlist,1,_nextlen + 1,'');
    END LOOP;
    
    /*close the query string*/
	set @res = concat(result, ') t where t.id=wi.id group by t.id order by sum(score)'); 

    
	/*join with wi in order to get ranked words*/
	set @res = concat('select word, sum(tf*idf) as rank from wi, (', @res, ') as t where wi.id = t.id group by word order by rank desc'); 
    
    /*prepare and execute the string with the sql query*/
    PREPARE stmt FROM @res;
    execute stmt;
end//
delimiter ;
call ranked_words('injection');