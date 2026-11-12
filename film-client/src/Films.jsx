import { useState, useEffect } from "react";

const API = "https://localhost:7150/api";

const noImage =
  "data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='300' height='450'><rect width='100%' height='100%' fill='%23302b63'/><text x='50%' y='50%' fill='white' font-size='24' text-anchor='middle'>No image</text></svg>";

function Films({ onLogout }) {
  const [films, setFilms] = useState([]);
  const [showForm, setShowForm] = useState(false);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState("");
  const [message, setMessage] = useState("");
  const [listError, setListError] = useState("");

  async function loadFilms() {
    try {
      const response = await fetch(API + "/Film");
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
    if (title === "" || imageUrl === "") {
      setMessage("Вкажіть назву фільму та посилання на картинку");
      return;
    }

    try {
      const response = await fetch(API + "/Film", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          title: title,
          description: description,
          imageUrl: imageUrl,
        }),
      });

      if (response.ok) {
        setTitle("");
        setDescription("");
        setImageUrl("");
        setShowForm(false);
        setMessage("");
        loadFilms();
      } else {
        setMessage("Не вдалося додати фільм. Перевірте дані");
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
      <div className="top">
        <h1>My Films</h1>
        <div className="top-buttons">
          <button onClick={() => setShowForm(!showForm)}>+ Add Film</button>
          <button className="logout-btn" onClick={onLogout}>
            Logout
          </button>
        </div>
      </div>

      {showForm && (
        <div className="add-form">
          <input
            placeholder="Title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
          <input
            placeholder="Description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
          />
          <input
            placeholder="Image URL"
            value={imageUrl}
            onChange={(e) => setImageUrl(e.target.value)}
          />
          <button onClick={addFilm}>Save</button>
          {message && <p className="error">{message}</p>}
        </div>
      )}

      {listError && <p className="error">{listError}</p>}

      <div className="cards">
        {films.length === 0 && !listError && (
          <p className="empty">Фільмів поки немає. Натисни Add Film</p>
        )}

        {films.map((film) => (
          <div className="card" key={film.id}>
            <div className="card-image">
              <img
                src={film.imageUrl}
                alt={film.title}
                onError={(e) => {
                  e.target.onerror = null;
                  e.target.src = noImage;
                }}
              />
            </div>
            <div className="card-body">
              <h3>{film.title}</h3>
              <p>{film.description}</p>
              <button className="remove-btn" onClick={() => removeFilm(film.id)}>
                Remove ✕
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Films;