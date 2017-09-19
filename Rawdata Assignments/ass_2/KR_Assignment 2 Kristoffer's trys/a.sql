use raw2;

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