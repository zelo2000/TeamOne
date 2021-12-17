import React, { FC } from "react";
import { Redirect, Route } from "react-router-dom";
import { GetAuthData } from "../utils/storage-helper";

interface IProtectedRouteProps {
  children: React.ReactNode;
  exact?: boolean;
  path: string;
}

const ProtectedRoute: FC<IProtectedRouteProps> = ({ children, exact, path, ...restOfProps }: IProtectedRouteProps) => {
  const isAuthenticated = GetAuthData();

  return (
    <Route
      {...restOfProps}
      exact
      path={path}
      render={() =>
        isAuthenticated ? children : <Redirect to="/" />
      }
    />
  );
}

export default ProtectedRoute;