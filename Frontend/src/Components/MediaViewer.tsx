import { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { Movie } from "../Models/Movie";

export const MediaViewer = () => {
  const [movie, setMovie] = useState<Movie>(null);
  const [searchParams] = useSearchParams();

  useEffect(() => {
    setMovie({
      title: searchParams.get("title"),
      year: parseInt(searchParams.get("year"))
    });
  }, []);

  return (
    <>
      {movie ? (
        <div>
          <video width="640" height="480" controls playsInline>
              <source type="video/mp4" 
                src={`${process.env.REACT_APP_BACKEND_URL}/api/Movie/stream?title=${movie.title}&year=${movie.year}`} 
              />
          </video>
        </div>
      )
      :
        null
      }
    </>
  );
}