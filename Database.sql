
CREATE TABLE genres (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE movies (
    id SERIAL PRIMARY KEY,
    title VARCHAR(200) NOT NULL,
    genre_id INT REFERENCES genres(id),
    release_year INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE TABLE reviews (
    id SERIAL PRIMARY KEY,
    movie_id INT REFERENCES movies(id),
    username VARCHAR(100),
    comment TEXT,
    rating DECIMAL(2,1),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE INDEX idx_genres_name ON genres(name);

CREATE INDEX idx_movies_genre_id ON movies(genre_id);

CREATE INDEX idx_movies_release_year ON movies(release_year);

CREATE INDEX idx_reviews_movie_id ON reviews(movie_id);


INSERT INTO genres (name) VALUES ('Action'), ('Comedy'), ('Drama'), ('Horror'), ('Romance');

INSERT INTO movies (title, genre_id, release_year) VALUES
('Inception', 5, 2010),
('The Dark Knight', 1, 2008),
('Parasite', 3, 2019),
('Get Out', 4, 2017),
('Interstellar', 5, 2014);

INSERT INTO reviews (movie_id, username, comment, rating) VALUES
(1, 'Alice', 'Mind-blowing sci-fi!', 4.8),
(2, 'Bob', 'Best Batman movie ever', 5.0),
(3, 'Charlie', 'Social commentary at its finest', 4.7),
(4, 'Dina', 'Creepy and smart horror', 4.5),
(5, 'Edo', 'Epic space journey', 4.9);


select * from movies
select * from genres
select * from reviews


drop table genres