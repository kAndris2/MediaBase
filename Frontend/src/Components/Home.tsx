import { useContext, useEffect, useState } from "react";
import { ServiceContext } from "..";
import { Movie } from "../Models/Movie";
import { v4 as uuidv4 } from 'uuid';
import { Link } from "react-router-dom";

import "../../public/css/home.css";

export const Home = () => {
    const [movies, setMovies] = useState<Movie[]>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const servicesContext = useContext(ServiceContext);

    useEffect(() => {
        async function init() {
            const _movies = await servicesContext.movieBackendService.getTitles();
            setMovies(_movies);
            setLoading(false);
        }
        init();
    }, []);

    if (loading)
        return (<h1>Loading...</h1>);

    return (
        <div className="container">
            <div className="titles">
                {movies.map(movie => (
                    <div className="title" key={uuidv4()}>
                        <Link to={`/movie?title=${movie.title}&year=${movie.year}`}>
                            {movie.title}
                        </Link>
                    </div>
                ))}
            </div>
        </div>
    );
}