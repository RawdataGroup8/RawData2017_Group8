use imdb2016;
/*a*/
#select title, production_year from title join kind_type on (title.kind_id=kind_type.id) where title like 'Pirates of the Caribbean%' and kind='movie';

/*b*/
#select count(title) from title join kind_type on (title.kind_id=kind_type.id) where production_year=2004 and kind='movie';

/*c*/
#select title from title join kind_type on (title.kind_id=kind_type.id) join cast_info on (title.id=cast_info.movie_id) join name on (cast_info.person_id=name.id) where name='Mikkelsen, Mads' and kind='video game';

/*d*/
#select distinct role from role_type join cast_info on(role_type.id=cast_info.role_id) join name on(cast_info.person_id=name.id) where name='Bacon, Kevin';

/*e*/
#select role, count(cast_info.role_id) as number from role_type join cast_info on(role_type.id=cast_info.role_id) group by role order by number desc;

/*f*/
#select title from title join cast_info on(title.id=cast_info.movie_id) join role_type on (role_type.id=cast_info.role_id) join name on (cast_info.person_id=name.id) where name='Scott, Ridley' and (production_year=2006 or production_year=2008 or production_year=2010) and role='director';

/*g*/ /*timeouts*/
#select max(count(person_id)) from cast_info join title on (cast_info.movie_id = title.id) group by (movie_id);

#select max(person_id) from
#(select count(person_id) as output from cast_info group by (movie_id) /*order by (output) desc*/) as person_id_count
#;

/*h*/ /*timeouts*/
#select count(person_id) from cast_info join title on(cast_info.movie_id = title.id) where title in
#(select title from title join cast_info on (title.id = cast_info.movie_id) join name on (cast_info.person_id = name.id) where name = 'Bacon, Kevin')
#;

#select count(otheres.person_id) from cast_info as otheres where movie_id in (
#select  movie_id from cast_info join name on (cast_info.person_id = name.id) where name = 'Bacon, Kevin'
#);

/*i*//*close but not quiet right, (#1.3) (#1.8) and The Runaways are have both keywords but aren't in the surgestet output in the assignment description*/
#select title from title join movie_keyword on (movie_keyword.movie_id = title.id) join keyword on (movie_keyword.keyword_id=keyword.id) where keyword='sister' and title in (select title from title join movie_keyword on (movie_keyword.movie_id = title.id) join keyword on (movie_keyword.keyword_id=keyword.id) where keyword='elephant');

/*j*/
#select title, production_year from title join movie_companies on (title.id = movie_companies.movie_id) join company_name on (movie_companies.company_id = company_name.id) where name = 'Paramount' and country_code = '[se]' and production_year > 2004 order by (title);

/*k*/
#select title from title join cast_info on (title.id = cast_info.movie_id) join char_name on (cast_info.person_role_id = char_name.id) where name = 'The Singing Kid';