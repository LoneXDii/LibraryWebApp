import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import HomePage from './Components/Pages/HomePage';
import BooksPage from './Components/Pages/BooksPage/BooksPage';
import ProfilePage from './Components/Pages/ProfilePages/ProfilePage';
import 'bootstrap/dist/css/bootstrap.min.css'
import LoginAndSingupPage from './Components/Pages/ProfilePages/LoginAndSignupPage/LoginAndSignupPage';
import { AuthenticationService } from './Services/AuthenticationService';
import LogoutPage from './Components/Pages/ProfilePages/LogoutPage';

AuthenticationService.loadUserFromStorage();

const router = createBrowserRouter([
  {
    path: "",
    element: <App />,
  },
  {
    path: "/home",
    element: <HomePage />,
  },
  {
    path: "/books",
    element: <BooksPage />,
  },
  {
    path: "/profile",
    element: <ProfilePage />,
  },
  {
    path: "/login",
    element: <LoginAndSingupPage/>,
  },
  {
    path: "/logout",
    element: <LogoutPage/>,
  },
]);


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
