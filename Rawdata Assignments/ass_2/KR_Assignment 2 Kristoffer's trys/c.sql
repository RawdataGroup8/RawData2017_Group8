use raw2;

drop procedure if exists movie_match_title;

delimiter //
create procedure movie_match_title(title varchar(20))
begin
	select *
    from imdb2016.title t, imdb2016.kind_type k
    where t.title like title
		and t.kind_id = k.id
        and k.kind = 'movie'
		and t.production_year <= year(curdate())
    order by t.production_year desc
    limit 10;
end;//

delimiter ;

call movie_match_title('%the%');

drop procedure if exists movie_match_title;