-- Assignment 1 (Due 14/9)
use imdb2016;

-- a) Find the titles and production year of all movies with a title starting with ‘Pirates of the Caribbean’.
select title, production_year from title where title like 'Pirates of the Caribbean%' and kind_id = 1 order by production_year;

-- b) How many movies were produced in 2004?
select count(*) from title where production_year = 2004 and kind_id = 1;

-- c) Find the title of all video games with Mads Mikkelsen.
select title from title join (select distinct movie_id from cast_info join name on name.id = cast_info.person_id where name = "Mikkelsen, Mads") as madsMIDs on madsMIDs.movie_id = title.id where kind_id = 6;
select title from (title join cast_info on movie_id=title.id) join name on name.id = cast_info.person_id where name = "Mikkelsen, Mads" and kind_id = 6;
select title from title, cast_info, name where name.id = person_id and movie_id=title.id and name = "Mikkelsen, Mads" and kind_id = 6;

-- d) Find the different roles that Kevin Bacon has had.
select role from role_type join(select distinct role_id from cast_info join name on name.id = cast_info.person_id where name = "Bacon, Kevin") as baconRoles on role_id = id;
select distinct role from role_type, cast_info, name where name.id = cast_info.person_id and name="Bacon, Kevin" and role_id = role_type.id;

-- e) Find for each role the number of persons assigned to the roles order descending by the number.
select role, count(*) as count from cast_info join role_type on role_type.id = cast_info.role_id group by role_id order by count desc;

-- f) Find the title of titles directed by Ridley Scott from 2004, 2006, 2008 or 2010.
select title from cast_info, name, title 
where name = "Scott, Ridley" and name.id = cast_info.person_id 
and role_id = 8 and title.id = cast_info.movie_id 
and mod(production_year, 2) = 0 and production_year between 2004 and 2010;

-- g) What is the highest number of persons casted for one title?
select max(casted.num) as MaxCastNum from (select count(*) as num from cast_info group by movie_id) as casted; -- ~30 secs

-- h) How many actors has Kevin Bacon acted together with? 
select count(distinct c2.person_id) as BaconsFriends
from cast_info c1, cast_info c2, name 
where c1.movie_id = c2.movie_id 
and c1.person_id = name.id and c2.person_id != name.id and name.name = "Bacon, Kevin" -- 
and c1.role_id = 1 and c2.role_id = 1;

-- i) Title of all titles that are assigned to the keywords ‘sister’ and ‘elephant’
select title from (
(select movie_id from keyword join movie_keyword on keyword.id = movie_keyword.keyword_id where keyword = 'elephant') as ele
inner join 
(select movie_id from keyword join movie_keyword on keyword.id = movie_keyword.keyword_id where keyword = 'sister') as sis)
join title on ele.movie_id = id and sis.movie_id = id;

select title from keyword k1, keyword k2, movie_keyword mk1, movie_keyword mk2, title t
where t.id = mk1.movie_id and t.id = mk2.movie_id  
	and k1.keyword = 'sister' and mk1.id = k1.id
    and k2.keyword = 'elephant' and mk2.id = k2.id;

-- j) Find the title and production year of movies from “Paramount” in Sweden produced after 2004 ordered by title.
select title, production_year from title, company_name, movie_companies 
where company_name.id = movie_companies.company_id and name = "Paramount" and country_code = "[se]" and title.id = movie_id and production_year > 2004
order by title asc;

-- k) Find the title of movies that have casted a character named “The Singing Kid”
select title from cast_info, char_name, title where cast_info.person_role_id = char_name.id and name = "The Singing Kid" and cast_info.movie_id = title.id;

