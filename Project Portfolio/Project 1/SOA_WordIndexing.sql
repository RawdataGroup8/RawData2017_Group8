-- MySQL dump 10.13  Distrib 5.7.9, for osx10.9 (x86_64)
--
-- Host: localhost    Database: stackoverflow_sample
-- ------------------------------------------------------
-- Server version	5.7.10
use stack_overflow_normalized;
/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `stopwords`
--

DROP TABLE IF EXISTS `stopwords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stopwords` (
  `word` varchar(18) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stopwords`
--

LOCK TABLES `stopwords` WRITE;
/*!40000 ALTER TABLE `stopwords` DISABLE KEYS */;
INSERT INTO `stopwords` VALUES ('a\'s'),('able'),('about'),('above'),('according'),('accordingly'),('across'),('actually'),('after'),('afterwards'),('again'),('against'),('ain\'t'),('all'),('allow'),('allows'),('almost'),('alone'),('along'),('already'),('also'),('although'),('always'),('am'),('among'),('amongst'),('an'),('and'),('another'),('any'),('anybody'),('anyhow'),('anyone'),('anything'),('anyway'),('anyways'),('anywhere'),('apart'),('appear'),('appreciate'),('appropriate'),('are'),('aren\'t'),('around'),('as'),('aside'),('ask'),('asking'),('associated'),('at'),('available'),('away'),('awfully'),('be'),('became'),('because'),('become'),('becomes'),('becoming'),('been'),('before'),('beforehand'),('behind'),('being'),('believe'),('below'),('beside'),('besides'),('best'),('better'),('between'),('beyond'),('both'),('brief'),('but'),('by'),('c\'mon'),('c\'s'),('came'),('can'),('can\'t'),('cannot'),('cant'),('cause'),('causes'),('certain'),('certainly'),('changes'),('clearly'),('co'),('com'),('come'),('comes'),('concerning'),('consequently'),('consider'),('considering'),('contain'),('containing'),('contains'),('corresponding'),('could'),('couldn\'t'),('course'),('currently'),('definitely'),('described'),('despite'),('did'),('didn\'t'),('different'),('do'),('does'),('doesn\'t'),('doing'),('don\'t'),('done'),('down'),('downwards'),('during'),('each'),('edu'),('eg'),('eight'),('either'),('else'),('elsewhere'),('enough'),('entirely'),('especially'),('et'),('etc'),('even'),('ever'),('every'),('everybody'),('everyone'),('everything'),('everywhere'),('ex'),('exactly'),('example'),('except'),('far'),('few'),('fifth'),('first'),('five'),('followed'),('following'),('follows'),('for'),('former'),('formerly'),('forth'),('four'),('from'),('further'),('furthermore'),('get'),('gets'),('getting'),('given'),('gives'),('go'),('goes'),('going'),('gone'),('got'),('gotten'),('greetings'),('had'),('hadn\'t'),('happens'),('hardly'),('has'),('hasn\'t'),('have'),('haven\'t'),('having'),('he'),('he\'s'),('hello'),('help'),('hence'),('her'),('here'),('here\'s'),('hereafter'),('hereby'),('herein'),('hereupon'),('hers'),('herself'),('hi'),('him'),('himself'),('his'),('hither'),('hopefully'),('how'),('howbeit'),('however'),('i\'d'),('i\'ll'),('i\'m'),('i\'ve'),('ie'),('if'),('ignored'),('immediate'),('in'),('inasmuch'),('inc'),('indeed'),('indicate'),('indicated'),('indicates'),('inner'),('insofar'),('instead'),('into'),('inward'),('is'),('isn\'t'),('it'),('it\'d'),('it\'ll'),('it\'s'),('its'),('itself'),('just'),('keep'),('keeps'),('kept'),('know'),('knows'),('known'),('last'),('lately'),('later'),('latter'),('latterly'),('least'),('less'),('lest'),('let'),('let\'s'),('like'),('liked'),('likely'),('little'),('look'),('looking'),('looks'),('ltd'),('mainly'),('many'),('may'),('maybe'),('me'),('mean'),('meanwhile'),('merely'),('might'),('more'),('moreover'),('most'),('mostly'),('much'),('must'),('my'),('myself'),('name'),('namely'),('nd'),('near'),('nearly'),('necessary'),('need'),('needs'),('neither'),('never'),('nevertheless'),('new'),('next'),('nine'),('no'),('nobody'),('non'),('none'),('noone'),('nor'),('normally'),('not'),('nothing'),('novel'),('now'),('nowhere'),('obviously'),('of'),('off'),('often'),('oh'),('ok'),('okay'),('old'),('on'),('once'),('one'),('ones'),('only'),('onto'),('or'),('other'),('others'),('otherwise'),('ought'),('our'),('ours'),('ourselves'),('out'),('outside'),('over'),('overall'),('own'),('particular'),('particularly'),('per'),('perhaps'),('placed'),('please'),('plus'),('possible'),('presumably'),('probably'),('provides'),('que'),('quite'),('qv'),('rather'),('rd'),('re'),('really'),('reasonably'),('regarding'),('regardless'),('regards'),('relatively'),('respectively'),('right'),('said'),('same'),('saw'),('say'),('saying'),('says'),('second'),('secondly'),('see'),('seeing'),('seem'),('seemed'),('seeming'),('seems'),('seen'),('self'),('selves'),('sensible'),('sent'),('serious'),('seriously'),('seven'),('several'),('shall'),('she'),('should'),('shouldn\'t'),('since'),('six'),('so'),('some'),('somebody'),('somehow'),('someone'),('something'),('sometime'),('sometimes'),('somewhat'),('somewhere'),('soon'),('sorry'),('specified'),('specify'),('specifying'),('still'),('sub'),('such'),('sup'),('sure'),('t\'s'),('take'),('taken'),('tell'),('tends'),('th'),('than'),('thank'),('thanks'),('thanx'),('that'),('that\'s'),('thats'),('the'),('their'),('theirs'),('them'),('themselves'),('then'),('thence'),('there'),('there\'s'),('thereafter'),('thereby'),('therefore'),('therein'),('theres'),('thereupon'),('these'),('they'),('they\'d'),('they\'ll'),('they\'re'),('they\'ve'),('think'),('third'),('this'),('thorough'),('thoroughly'),('those'),('though'),('three'),('through'),('throughout'),('thru'),('thus'),('to'),('together'),('too'),('took'),('toward'),('towards'),('tried'),('tries'),('truly'),('try'),('trying'),('twice'),('two'),('un'),('under'),('unfortunately'),('unless'),('unlikely'),('until'),('unto'),('up'),('upon'),('us'),('use'),('used'),('useful'),('uses'),('using'),('usually'),('value'),('various'),('very'),('via'),('viz'),('vs'),('want'),('wants'),('was'),('wasn\'t'),('way'),('we'),('we\'d'),('we\'ll'),('we\'re'),('we\'ve'),('welcome'),('well'),('went'),('were'),('weren\'t'),('what'),('what\'s'),('whatever'),('when'),('whence'),('whenever'),('where'),('where\'s'),('whereafter'),('whereas'),('whereby'),('wherein'),('whereupon'),('wherever'),('whether'),('which'),('while'),('whither'),('who'),('who\'s'),('whoever'),('whole'),('whom'),('whose'),('why'),('will'),('willing'),('wish'),('with'),('within'),('without'),('won\'t'),('wonder'),('would'),('wouldn\'t'),('yes'),('yet'),('you'),('you\'d'),('you\'ll'),('you\'re'),('you\'ve'),('your'),('yours'),('yourself'),('yourselves'),('zero');
/*!40000 ALTER TABLE `stopwords` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-04-11 20:39:46


-- All the following procedures use the same (rather extensive) method to allow comma separated strings with any number of values as input.

/* B.1 This procedure retrieves posts that simply match words in the query*/
-- use raw8;
use stack_overflow_normalized;
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

