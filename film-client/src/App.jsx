import { useState } from "react";
import Login from "./Login";
import Register from "./Register";
import Films from "./Films";
import "./App.css";

function App() {
  const [page, setPage] = useState(
    localStorage.getItem("loggedIn") === "true" ? "films" : "login"
  );

  function handleLoginSuccess() {
    localStorage.setItem("loggedIn", "true");
    setPage("films");
  }

  function handleLogout() {
    localStorage.removeItem("loggedIn");
    setPage("login");
  }

  if (page === "films") {
    return <Films onLogout={handleLogout} />;
  }

  if (page === "register") {
    return <Register goLogin={() => setPage("login")} />;
  }

  return (
    <Login
      onSuccess={handleLoginSuccess}
      goRegister={() => setPage("register")}
    />
  );
}

export default App;