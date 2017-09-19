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