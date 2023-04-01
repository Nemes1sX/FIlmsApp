import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import FilmsIndex  from "./components/Films/FilmsIndex";
import { FilmRead } from "./components/Films/FilmRead";
import { FilmForm } from "./components/Films/FilmForm";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
    },
    {
        path: '/film',
        element: <FilmsIndex/>
    },
    {
        path: '/film/read/:id',
        element: <FilmRead/>
    },
    {
        path: '/film/create',
        element: <FilmForm/>
    },
    {
        path: '/film/update/:id',
        element: <FilmForm/>
    }

];

export default AppRoutes;
