
-- All the following procedures use the same (rather extensive) method to allow comma separated strings with any number of values as input.

/* B.1 This procedure retrieves posts that simply match words in the query*/
use raw8;
drop procedure if exists simplematch;
delimiter //
create procedure simplematch (in _wlist varchar(5000))
begin 
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
    DECLARE result TEXT DEFAULT NULL;
    DECLARE firstIter BOOL DEFAULT TRUE;
    
    set result = 'select words.id from words, (';
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
        SET result = CONCAT(result, 'select distinct id from words where word = ', "'",trim(_next),"'");
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
drop table if exists mwib;
create table mwib as
select distinct id, idx, word, sen from words
where word regexp '^[A-Za-z][A-Za-z]{1,}$'
and tablename = 'posts' and what='body';
CREATE INDEX indexBody
ON mwib (word);
drop procedure if exists bestmatch;
delimiter //
create procedure bestmatch (in _wlist varchar(5000))
begin 
	DECLARE _next TEXT DEFAULT NULL;
	DECLARE _nextlen INT DEFAULT NULL;
    DECLARE result TEXT DEFAULT NULL;
    DECLARE firstIter BOOL DEFAULT TRUE;
    
    set result = 'select mwib.id, sum(score) rank, word from mwib, (';
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
	set @res = concat(result, ') t where t.id=mwib.id group by t.id order by rank desc limit 15'); 
    
    /*prepare and execute the string with the sql query*/
    PREPARE stmt FROM @res;
    execute stmt;
end//
delimiter ;
call bestmatch('mysql, procedures');

/*B.3 create word index with tf idf columns*/
drop table if exists ndt;
create table ndt as 
select id, word, count(*) as ndt_ FROM words where word regexp '^[A-Za-z][A-Za-z]{1,}$' and word not in (select * from stopwords) group by id, word;
create index ndtIndex on ndt(id, word);

drop table if exists nd;
create table nd as 
select distinct id, count(word) as nd_ FROM words where word regexp '^[A-Za-z][A-Za-z]{1,}$' and word not in (select * from stopwords) group by id;
create index ndIndex on nd(id);

drop table if exists nt;
create table nt as 
select word, count(distinct id) as nt_ from words where word regexp '^[A-Za-z][A-Za-z]{1,}$' and word not in (select * from stopwords) group by word;
create index ntIndex on nt(word);

drop table if exists wi;
create table wi as 
select id, word, nt_,/*ndt_, nd_, nt_,*/ log2(1+(ndt_/nd_)) as tf, (1.0/nt_) as idf, log2(1+(ndt_/nd_))/nt_ as tf_idf 
from words natural join ndt natural join nd natural join nt;
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
    -- from(select distinct id, 1 score from wi where word = '<word>') t1
    -- group by id) t2 where wi.id=t2.id and word not in (select * from stopwords) group by word order by r desc limit 10'
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

/*B.6. This procedure is almost the same as ranked_post_search, but just before prep/execute the required sql to retrieve ranked words is added*/
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

    
	/*join with wi in order to get ranked words instead of posts*/
	set @res = concat('select word, sum(tf_idf) as rank from wi, (', @res, ') as t where wi.id = t.id group by word order by rank desc'); 
    
    /*prepare and execute the string with the sql query*/
    PREPARE stmt FROM @res;
    execute stmt;
end//
delimiter ;
call ranked_words('csharp, python');

/* B.8. */
drop table if exists cooccuring;
create table cooccuring as 
select w1.word as word1, w2.word as word2,count(*) grade from wi w1,wi w2
where w1.id=w2.id and w1.word<w2.word
and w1.tf_idf>0.0002 and w2.tf_idf>0.0002 and w1.nt_>20 and w2.nt_>20
group by w1.word,w2.word order by count(*) desc;

/*B.9*/
drop procedure if exists term_network;
delimiter //
create procedure term_network(in w varchar(100))
begin
  set@rank=-1;
  create table nodes as select @rank:=@rank+1 num, word2 word from cooccuring where word1=w and grade>1;
  create index ix_nodex_word on nodes(word);
  select 'var graph = '
  union
  select '{"nodes":['
  union
  select ''
  union
  select concat('{"name":"',word,'"},') line from nodes
  union
  select '],'
  union
  select '"links":['
  union
  select concat('{"source":	',n1.num,'	,"target":	', n2.num,'	,"value":',grade,'},') line from cooccuring,nodes n1,nodes n2 
  where n1.word=word1 and n2.word=word2
  and word1 in (select word from nodes) and  word2 in (select word from nodes)
  union
  select ']}';
  drop table if exists nodes;
end//
delimiter ;
call term_network('xml');
