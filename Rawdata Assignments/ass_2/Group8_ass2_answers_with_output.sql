#Group 8 - Kristoffer Schou, Eugenio Capuani and Rune Barrett
use raw1;

-- ---------------------------------------- a)
drop function if exists movie_count;
delimiter //
create function movie_count (actor_name char(50)) 
returns integer
begin
declare mov_count integer;
SELECT 
    COUNT(DISTINCT movie_id)
INTO mov_count FROM imdb2016.cast_info c, imdb2016.name n, imdb2016.role_type r, imdb2016.title t
WHERE c.person_id = n.id AND c.role_id = r.id
        AND c.movie_id = t.id
        AND t.kind_id = 11234
        AND r.role = 'actor'
        AND n.name LIKE actor_name;
return mov_count;
end;//
delimiter ; 

SELECT MOVIE_COUNT('Bacon, Kevin');

/*
Execute:
> SELECT MOVIE_COUNT('Bacon, Kevin')

+ -------------------------------- +
| MOVIE_COUNT('Bacon, Kevin')      |
+ -------------------------------- +
| 90                               |
+ -------------------------------- +
1 rows
*/

-- ---------------------------------------- b)
drop procedure if exists movies;
delimiter //
create procedure movies (cast_name char(50))
begin
	select distinct title from imdb2016.title t, imdb2016.cast_info c, imdb2016.name n, imdb2016.kind_type
	where c.person_id = n.id
	and c.movie_id = t.id
    and t.kind_id = kind_type.id
    and kind_type.kind = 'movie'
	and name = cast_name;
end;//
delimiter ;

call movies('Mikkelsen, Mads');

Execute:
> call movies('Mikkelsen, Mads')

+ ---------- +
| title      |
+ ---------- +
| A Bond for Life: How James Bond Changed My Life |
| Adams æbler |
| Bleeder    |
| Blinkende lygter |
| Blomsterfangen |
| Café Hector |
| Casino Royale |
| Clash of the Titans |
| Coco Chanel & Igor Stravinsky |
| De grønne slagtere |
| Die Tür   |
| Doctor Strange |
| Dykkerdrengen |
| Efter brylluppet |
| Elsker dig for evigt |
| En kongelig affære |
| En kort en lang |
| Exit       |
| Flammen & Citronen |
| I Am Dina  |
| Jagten     |
| King Arthur |
| Michael Kohlhaas |
| Monas verden |
| Move On    |
| Muumi ja punainen pyrstötähti |
| Mænd & høns |
| Nattens engel |
| Nu         |
| Prag       |
| Pusher     |
| Pusher II  |
| Rogue One: A Star Wars Story |
| The Necessary Death of Charlie Countryman |
| The Salvation |
| The Three Musketeers |
| Tom Merritt |
| Torremolinos 73 |
| Valhalla Rising |
| Vildspor   |
| Wilbur Wants to Kill Himself |
+ ---------- +
41 rows


-- ---------------------------------------- c)
drop procedure if exists recent_movs;
delimiter //
create procedure recent_movs(titleText char(250))
begin
	select title from imdb2016.title, imdb2016.kind_type kt
    where title like titleText
    and title.kind_id = kt.id and kt.kind = 'movie'
    and production_year <= year(curdate())
    order by production_year desc 
    limit 10;
end;//
delimiter ;

call recent_movs('%the%');
/*
Execute:
> call recent_movs('%the%')

+ ---------- +
| title      |
+ ---------- +
| Back from the Edge of Life's Abyss |
| The Spike  |
| On the Take |
| The Barbie Effect |
| The Second Appearance |
| Vincent the Artist |
| The Missing Link |
| Chuck Steel: Night of the Trampires |
| The Living Universe |
| The Bally Girl |
+ ---------- +
10 rows


*/
-- ---------------------------------------- d)
drop function if exists roles;

delimiter //
create function roles(person_name varchar(32))
returns varchar(255)

begin
declare done int default false;
declare role_concat varchar(255) default "" ;
declare role_str varchar(32);
declare role_cursor cursor for SELECT DISTINCT role
	FROM imdb2016.cast_info
	JOIN imdb2016.name
	ON cast_info.person_id = name.id
	JOIN imdb2016.role_type
	ON cast_info.role_id = role_type.id
WHERE name like person_name;
declare continue handler for not found set done = true;

open role_cursor;
#set role_concat = "";
read_loop: loop
	fetch role_cursor into role_str;
    if done then
		leave read_loop;
	end if;
    if char_length(role_concat) > 0 then
	set role_concat = concat(role_concat,  ", ", role_str);
    else set role_concat = concat(role_concat, role_str);
    end if;
end loop;
return role_concat;
close role_cursor;
end;//
delimiter ;

select roles('Bacon, Kevin');

SELECT name,roles(name)
FROM imdb2016.name where name like 'De Niro, R%';
 
/*
Execute:
> select roles('Bacon, Kevin')

+ -------------------------- +
| roles('Bacon, Kevin')      |
+ -------------------------- +
| actor, director, producer, writer, cinematographer, editor |
+ -------------------------- +
1 rows

Execute:
> SELECT name,roles(name)
FROM imdb2016.name where name like 'De Niro, R%'

+ --------- + ---------------- +
| name      | roles(name)      |
+ --------- + ---------------- +
| De Niro, Raphael | actor, producer  |
| De Niro, Robert | actor, director, producer |
| De Niro, Rocco | actor            |
| de Niro, Ryan | cinematographer  |
+ --------- + ---------------- +
4 rows

*/

drop function if exists movie_count;
drop procedure if exists movies;
drop procedure if exists recent_movs;
drop function if exists roles;