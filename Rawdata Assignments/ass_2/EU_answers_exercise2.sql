create procedure split_string (in input varchar(20))
begin
declare actor_firstname varchar(20);
declare actor_lastname varchar(20);


 



drop function if exists movie_count;
delimiter //
create function movie_count(actor_name varchar(20))
returns integer
begin
declare m_count integer;
SELECT count(distinct movie_id) into m_count
FROM imdb2016.cast_info as c, imdb2016.name as n, imdb2016.role_type as r, imdb2016.title as t
WHERE c.person_id = n.id
 AND c.role_id = r.id
 AND c.movie_id = t.id
 AND t.kind_id=1
 AND r.role = 'actor'
 AND n.name like 'Bacon, Kevin';
#AND n.name = strcmp( concat('%',@a_ln,'%',@a_fn,'%'), n.name);
return m_count;
end;//
delimiter ;
select movie_count('Bacon, Kevin');




#ANSWER TO B
drop procedure if exists movies;
delimiter //
create procedure movies(in actor_name varchar(20))
begin
SELECT imdb2016.title.title
FROM imdb2016.cast_info, imdb2016.name, imdb2016.role_type, imdb2016.title
WHERE imdb2016.cast_info.person_id = imdb2016.name.id
AND imdb2016.cast_info.movie_id = imdb2016.title.id;
#AND imdb2016.name.name ="Bacon, Kevin";
end; //
delimiter ;

call movies('Kevi n, Bacon');
 
 
 
 #ANSWER TO C
drop procedure if exists top_ten;
delimiter //
create procedure top_ten(in movie_title varchar(20))
begin
SELECT distinct imdb2016.title.title, imdb2016.title.production_year 
from imdb2016.title join imdb2016.kind_type
where imdb2016.title.production_year < 2017 AND  imdb2016.kind_type.id=1 AND imdb2016.title.title like '%Forrest%Gump%'
order by imdb2016.title.production_year desc
limit 10;
end; //
delimiter ;

call top_ten('Forrest Gump')



#ANSWER TO D
drop function if exists actor_roles;
delimiter //
create function actor_roles(actorname char(50))
returns char(255)
begin
declare output_string char(255) default '';
declare professions char(255) default '';
declare done int default false;

declare cursor1 cursor  for SELECT DISTINCT role
FROM imdb2016.cast_info
 JOIN imdb2016.name
 ON imdb2016.cast_info.person_id = imdb2016.name.id
 JOIN imdb2016.role_type
 ON imdb2016.cast_info.role_id = imdb2016.role_type.id
WHERE imdb2016.name.name like 'Bacon, Kevin';
declare continue handler for not found set done = true;

open cursor1;
	read_loop: loop
		fetch cursor1 into professions;
			if done then
				leave read_loop;
			end if;
        set output_string := concat(output_string,', ', professions);
	end loop;
close cursor1;

return output_string;
end; //
delimiter ;

select actor_roles('Bacon, Kevin');