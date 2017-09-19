#assignment 2 by group 8: Eugenio Capuani, Rune Barrett and Kristoffer Schou
use raw2;
#a

drop function if exists movie_count;

delimiter //
create function movie_count(actor_name varchar(20))
returns integer
begin
	declare movie_count_result integer;
	select count(distinct movie_id) into movie_count_result
    from imdb2016.cast_info c, imdb2016.name n, imdb2016.role_type r, imdb2016.title t
    where c.person_id = n.id
		and c.role_id = r.id
        and c.movie_id = t.id
        and t.kind_id = 1
        and r.role = 'actor'
        and n.name like actor_name;
	return movie_count_result;
end;//
delimiter ;

SELECT MOVIE_COUNT('Bacon, Kevin');

drop function if exists movie_count;
Execute:
> SELECT MOVIE_COUNT('Bacon, Kevin')

+ -------------------------------- +
| MOVIE_COUNT('Bacon, Kevin')      |
+ -------------------------------- +
| 90                               |
+ -------------------------------- +
1 rows

#b
use raw2;

drop procedure if exists movie_count_proc;

delimiter //
create procedure movie_count_proc(actor_name varchar(20))
begin
	select title
    from imdb2016.cast_info c, imdb2016.name n, imdb2016.title t, imdb2016.kind_type k
    where c.person_id = n.id
		and c.movie_id = t.id
        and t.kind_id = k.id
		and k.kind = 'movie'
		and n.name like actor_name;
end;//
delimiter ;

call movie_count_proc('Mikkelsen, Mads');

drop procedure if exists movie_count_proc;
Execute:
> call movie_count_proc('Mikkelsen, Mads')

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


#c

drop procedure if exists movie_match_title;

delimiter //
create procedure movie_match_title(title varchar(20))
begin
	select * -- we could use title here since the discription isn't specific
    from imdb2016.title t, imdb2016.kind_type k
    where t.title like title
		and k.kind = 'movie'
		and t.production_year <= year(curdate())
    order by t.production_year desc
    limit 10;
end;//

delimiter ;

call movie_match_title('%the%');

drop procedure if exists movie_match_title;

Execute:
> call movie_match_title('%the%')

+ ------- + ---------- + --------------- + ------------ + -------------------- + ------------ + ------------------ + ------------------ + -------------- + --------------- + ----------------- + ----------- + ------- + --------- +
| id      | title      | imdb_index      | kind_id      | production_year      | imdb_id      | phonetic_code      | episode_of_id      | season_nr      | episode_nr      | series_years      | md5sum      | id      | kind      |
+ ------- + ---------- + --------------- + ------------ + -------------------- + ------------ + ------------------ + ------------------ + -------------- + --------------- + ----------------- + ----------- + ------- + --------- +
| 2495488 | Back from the Edge of Life's Abyss |                 | 1            | 2017                 |              | B2165              |                    |                |                 |                   | 4cc51c5d873a46034fab82e060d60080 | 1       | movie     |
| 3421185 | The Spike  |                 | 1            | 2017                 |              | S12                |                    |                |                 |                   | 0761d803abb159c062e86ddbf8a9405b | 1       | movie     |
| 3102465 | On the Take |                 | 1            | 2017                 |              | O532               |                    |                |                 |                   | 58bc509b67aa11f349963b2a5007e321 | 1       | movie     |
| 3330818 | The Barbie Effect |                 | 1            | 2017                 |              | B6123              |                    |                |                 |                   | d770fab9792a474c511046495cf50695 | 1       | movie     |
| 3415555 | The Second Appearance |                 | 1            | 2017                 |              | S2531              |                    |                |                 |                   | 885916c97a9ff6d2c265dbedcd2f914c | 1       | movie     |
| 3506435 | Vincent the Artist |                 | 1            | 2017                 |              | V5253              |                    |                |                 |                   | 99296dbebb598ba10e89461eaee7b47b | 1       | movie     |
| 3393283 | The Missing Link |                 | 1            | 2017                 |              | M2524              |                    |                |                 |                   | 83c834d6f034ab4c49514399a572118b | 1       | movie     |
| 2584068 | Chuck Steel: Night of the Trampires |                 | 1            | 2017                 |              | C2345              |                    |                |                 |                   | 689a0ef979696342d24e52d2a11cf6ce | 1       | movie     |
| 3384580 | The Living Universe |                 | 1            | 2017                 |              | L1525              |                    |                |                 |                   | a1a511d411e991eee40fc9cb2592a3e3 | 1       | movie     |
| 3330564 | The Bally Girl |                 | 1            | 2017                 |              | B4264              |                    |                |                 |                   | 2b471a32b348f2fc74fe4084792e48a0 | 1       | movie     |
+ ------- + ---------- + --------------- + ------------ + -------------------- + ------------ + ------------------ + ------------------ + -------------- + --------------- + ----------------- + ----------- + ------- + --------- +
10 rows



#d

drop function if exists roles;

delimiter //
create function roles(actor_name varchar(20))
returns varchar(900)
begin
	declare roles_string varchar(900) default ' ';
    declare roles varchar(20);
    declare done int default false;
	declare cur cursor for SELECT DISTINCT role
		FROM imdb2016.cast_info c
			JOIN imdb2016.name n
				ON c.person_id = n.id
			JOIN imdb2016.role_type r
				ON c.role_id = r.id
		WHERE n.name like actor_name;
	declare continue handler for not found set done = true;
        
	open cur;
	read_loop: loop
		fetch cur into roles; 
		if done then 
			leave read_loop;
		end if;
        if length(roles_string) > 1 then
			set roles_string = concat(roles_string, ', ', roles);
        else
			set roles_string = roles;
		end if;
	end loop;
        
	close cur;
        
	return roles_string;
end; //
delimiter ;

#SELECT ROLES('Bacon, Kevin');

SELECT name,roles(name)
FROM imdb2016.name where name like 'De Niro, R%';

drop function if exists roles;

Execute:
> #SELECT ROLES('Bacon, Kevin');

SELECT name,roles(name)
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

