use kcschou;

drop procedure if exists movie_match_title;

delimiter //
create procedure movie_match_title(title varchar(20))
begin
	select *
    from imdb2016.title t, imdb2016.kind_type k
    where t.title like /*title*/'%the%'
    and k.id = 1
    and t.production_year <= year(curdate())
    order by t.production_year desc
    limit 10;
end;//

call movie_match_title('%the%');