import { useState } from "react";

const API = "https://localhost:7150/api";

function Login({ onSuccess, goRegister }) {
  const [login, setLogin] = useState("");
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");
  const [isError, setIsError] = useState(false);

  async function handleLogin() {
    try {
      const response = await fetch(API + "/User/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ login: login, password: password }),
      });

      let text = await response.text();

      if (response.ok) {
        setIsError(false);
        setMessage(text);
        setTimeout(onSuccess, 1000);
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
      <h2>LOGIN</h2>
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
      <div className="buttons">
        <button onClick={handleLogin}>Login</button>
        <button onClick={goRegister}>Register</button>
      </div>
      {message && <p className={isError ? "error" : "success"}>{message}</p>}
    </div>
  );
}

export default Login;