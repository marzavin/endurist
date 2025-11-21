import { useState, ChangeEvent, FormEvent, CSSProperties } from "react";
import logo from "../assets/logo.png";
import { useAuth } from "../services/AuthProvider";
import { useNavigate } from "react-router";
import { PropagateLoader } from "react-spinners";
import "./SignIn.less";

const override: CSSProperties = {
  display: "block",
  margin: "2rem auto"
};

function SignIn() {
  const authProvider = useAuth();
  const navigate = useNavigate();
  const [input, setInput] = useState({
    name: "",
    password: ""
  });
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<boolean>(false);

  const handleSubmitEvent = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (input.name !== "" && input.password !== "") {
      setError(false);
      setLoading(true);
      authProvider
        .signIn({ name: input.name, password: input.password })
        .then((result) => {
          if (result) {
            navigate("/");
          } else {
            setError(true);
          }
          setLoading(false);
        });
    }
  };

  const handleInput = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;
    setInput((prev) => ({
      ...prev,
      [name]: value
    }));
  };

  return (
    <div className="px-0 container text-center">
      <main>
        <div className="app-sign-in">
          <div className="row justify-content-center">
            <div className="col-md-6 col-lg-4">
              <div className="app-panel py-5">
                <img className="mb-4" width="64" alt="logo.png" src={logo} />
                <h3>Welcome</h3>
                <p>Sign in by entering the information below</p>
                <form onSubmit={handleSubmitEvent}>
                  <div className="form-group">
                    <div className="icon d-flex align-items-center justify-content-center">
                      <i className="app-font-l bi bi-person-fill" />
                    </div>
                    <input
                      type="text"
                      className="form-control"
                      name="name"
                      placeholder="Username"
                      onChange={handleInput}
                      required
                    />
                  </div>
                  <div className="form-group">
                    <div className="icon d-flex align-items-center justify-content-center">
                      <i className="app-font-l bi bi-lock-fill" />
                    </div>
                    <input
                      type="password"
                      className="form-control"
                      id="signin-password"
                      name="password"
                      placeholder="Password"
                      onChange={handleInput}
                      required
                    />
                  </div>
                  <div className="form-group">
                    <button
                      className="btn btn-primary form-control"
                      type="submit"
                    >
                      Sign In
                    </button>
                  </div>
                  {error && (
                    <p className="app-error-text">
                      Invalid email or password. Please try again.
                    </p>
                  )}
                  <PropagateLoader
                    color="#f48221"
                    cssOverride={override}
                    loading={loading}
                  ></PropagateLoader>
                </form>
              </div>
            </div>
          </div>
        </div>
      </main>
    </div>
  );
}

export default SignIn;
