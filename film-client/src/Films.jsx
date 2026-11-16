import { useState, useEffect } from "react";

const API = "https://localhost:7150/api";

const noImage =
    "data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='300' height='450'><rect width='100%' height='100%' fill='%23141416'/><text x='50%' y='50%' fill='%238d8a83' font-family='sans-serif' font-size='16' text-anchor='middle'>No Poster</text></svg>";

function Films({ onLogout }) {
    const [films, setFilms] = useState([]);
    const [showForm, setShowForm] = useState(false);
    const [title, setTitle] = useState("");
    const [description, setDescription] = useState("");
    const [posterUrl, setPosterUrl] = useState("");
    const [movieUrl, setMovieUrl] = useState("");
    const [releaseYear, setReleaseYear] = useState("");
    const [rating, setRating] = useState("");
    const [message, setMessage] = useState("");
    const [listError, setListError] = useState("");

    async function loadFilms() {
        try {
            const response = await fetch(API + "/Film");

            if (!response.ok) {
                setListError("Не вдалося завантажити фільми");
                return;
            }

            const data = await response.json();
            setFilms(data);
            setListError("");
        } catch (error) {
            setListError("Сервер недоступний. Не вдалося завантажити фільми");
        }
    }

    useEffect(() => {
        loadFilms();
    }, []);

    async function addFilm() {
        try {
            const response = await fetch(API + "/Film", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({
                    title: title,
                    description: description,
                    posterUrl: posterUrl,
                    movieUrl: movieUrl,
                    releaseYear: Number(releaseYear),
                    rating: Number(rating),
                }),
            });

            if (response.ok) {
                setTitle("");
                setDescription("");
                setPosterUrl("");
                setMovieUrl("");
                setReleaseYear("");
                setRating("");
                setShowForm(false);
                setMessage("");
                loadFilms();
            } else {
                const data = await response.json();
                let text = "";

                if (Array.isArray(data)) {
                    for (let i = 0; i < data.length; i++) {
                        text = text + data[i].errorMessage + " ";
                    }
                }

                if (text === "") {
                    text = "Не вдалося додати фільм. Перевірте дані";
                }

                setMessage(text);
            }
        } catch (error) {
            setMessage("Сервер недоступний");
        }
    }

    async function removeFilm(id) {
        try {
            await fetch(API + "/Film/" + id, { method: "DELETE" });
            loadFilms();
        } catch (error) {
            setListError("Не вдалося видалити фільм");
        }
    }

    return (
        <div className="films-page">
            <header className="top">
                <div className="brand">
                    <span className="brand-subtitle">PRIVATE CINEMA</span>
                    <h1>My Films</h1>
                </div>
                <div className="top-buttons">
                    <button onClick={() => setShowForm(!showForm)}>
                        {showForm ? "Close Form" : "+ Add Film"}
                    </button>
                    <button className="logout-btn" onClick={onLogout}>
                        Logout
                    </button>
                </div>
            </header>

            {showForm && (
                <div className="add-form">
                    <h3>Новий фільм</h3>
                    <input
                        placeholder="Назва фільму (Title)"
                        value={title}
                        onChange={(e) => setTitle(e.target.value)}
                    />
                    <input
                        placeholder="Опис (Description)"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                    <input
                        placeholder="Посилання на постер (https://...)"
                        value={posterUrl}
                        onChange={(e) => setPosterUrl(e.target.value)}
                    />
                    <input
                        placeholder="Посилання на фільм (https://...)"
                        value={movieUrl}
                        onChange={(e) => setMovieUrl(e.target.value)}
                    />
                    <div className="form-row">
                        <input
                            type="number"
                            placeholder="Рік випуску"
                            value={releaseYear}
                            onChange={(e) => setReleaseYear(e.target.value)}
                        />
                        <input
                            type="number"
                            step="0.1"
                            placeholder="Рейтинг (0 - 10)"
                            value={rating}
                            onChange={(e) => setRating(e.target.value)}
                        />
                    </div>
                    <button onClick={addFilm}>Зберегти фільм</button>
                    {message && <p className="error">{message}</p>}
                </div>
            )}

            {listError && <p className="error">{listError}</p>}

            <div className="cards">
                {films.length === 0 && !listError && (
                    <p className="empty">Фільмів поки немає. Натисни + Add Film</p>
                )}

                {films.map((film) => (
                    <article className="card" key={film.id}>
                        <div className="card-image">
                            <img
                                src={film.posterUrl}
                                alt={film.title}
                                onError={(e) => {
                                    e.target.onerror = null;
                                    e.target.src = noImage;
                                }}
                            />
                            <div className="card-overlay"></div>
                            {film.rating > 0 && (
                                <span className="rating-badge">★ {film.rating}</span>
                            )}
                            {film.releaseYear > 0 && (
                                <span className="year-badge">{film.releaseYear}</span>
                            )}
                        </div>
                        <div className="card-body">
                            <h3>{film.title}</h3>
                            <p>{film.description}</p>
                            <div className="card-actions">
                                <a
                                    className="watch-link"
                                    href={film.movieUrl}
                                    target="_blank"
                                    rel="noreferrer"
                                >
                                    Дивитися
                                </a>
                                <button
                                    className="remove-btn"
                                    onClick={() => removeFilm(film.id)}
                                    title="Видалити фільм"
                                >
                                    Видалити
                                </button>
                            </div>
                        </div>
                    </article>
                ))}
            </div>
        </div>
    );
}

export default Films;