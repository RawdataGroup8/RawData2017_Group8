#QUESTION A
SELECT 
    title, production_year
FROM
    movie.movie
WHERE
    title LIKE 'Pirates of the Caribbean%';

#Question B
SELECT 
    COUNT(title)
FROM
    movie.movie
WHERE
    production_year = 2004;
    
#Question C
SELECT title 
FROM title 
WHERE kind_id = (SELECT id FROM kind_type WHERE kind = "video game") AND title.id = ANY (SELECT movie_id FROM cast_info WHERE person_id = (SELECT id FROM name WHERE name.name LIKE '%Mikkelsen%Mads%'));