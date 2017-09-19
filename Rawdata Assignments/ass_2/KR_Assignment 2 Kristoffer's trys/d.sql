use raw2;

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