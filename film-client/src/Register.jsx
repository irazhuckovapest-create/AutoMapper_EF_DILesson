import { useState } from "react";

const API = "https://localhost:7150/api";

function Register({ goLogin }) {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [age, setAge] = useState("");
  const [message, setMessage] = useState("");
  const [isError, setIsError] = useState(false);

  async function handleRegister() {
    try {
      const response = await fetch(API + "/User/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          login: login,
          password: password,
          name: name,
          surname: surname,
          age: Number(age),
        }),
      });

      let text = await response.text();

      if (response.ok) {
        setIsError(false);
        setMessage(text);
      } else {
        if (text.startsWith("{")) {
          text = "Перевірте правильність введених даних";
        }
        setIsError(true);
        setMessage(text);
      }
    } catch (error) {
      setIsError(true);
      setMessage("Сервер недоступний");
    }
  }

  return (
    <div className="box">
      <h2>REGISTER</h2>
      <input
        placeholder="Login"
        value={login}
        onChange={(e) => setLogin(e.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <input
        placeholder="Name"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <input
        placeholder="Surname"
        value={surname}
        onChange={(e) => setSurname(e.target.value)}
      />
      <input
        type="number"
        placeholder="Age"
        value={age}
        onChange={(e) => setAge(e.target.value)}
      />
      <div className="buttons">
        <button onClick={handleRegister}>Register</button>
        <button onClick={goLogin}>Back to login</button>
      </div>
      {message && <p className={isError ? "error" : "success"}>{message}</p>}
    </div>
  );
}

export default Register;