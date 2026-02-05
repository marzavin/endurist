import { useAuth } from "react-oidc-context";
import { useEffect } from "react";
import { useNavigate } from "react-router";

function Callback() {
  const auth = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (auth.isAuthenticated) {
      navigate("/");
    }
  }, [auth.isAuthenticated]);

  if (auth.isLoading) {
    return <div>Signing in...</div>;
  }

  if (auth.error) {
    return <div>Error: {auth.error.message}</div>;
  }

  return null;
}

export default Callback;
