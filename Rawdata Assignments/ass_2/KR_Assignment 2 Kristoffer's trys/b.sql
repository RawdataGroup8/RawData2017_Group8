use kcschou;

drop procedure if exists movie_count_proc;

delimiter //
create procedure movie_count_proc(actor_name varchar(20))
begin
	select title
    from imdb2016.cast_info c, imdb2016.name n, imdb2016.title t
    where c.person_id = n.id
                        and c.movie_id = t.id
                        and t.kind_id = 1
                        and n.name like /*actor_name*/'Mikkelsen, Mads';
end;//
delimiter ;

call movie_count_proc('Mikkelsen, Mads');