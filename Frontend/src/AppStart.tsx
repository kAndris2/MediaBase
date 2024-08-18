import { Route, Routes } from "react-router-dom";
import { Home } from "./Components/Home";
import { MediaViewer } from "./Components/MediaViewer";

export const AppStart = () => {

  return (
    <Routes>
      <Route path={'/'} element={<Home />} />
      <Route path={'/movie/:input'} element={<MediaViewer />} />
      <Route path={'/series/:input'} element={<MediaViewer />} />
    </Routes>
  );
}