import { Route, Routes } from "react-router-dom";
import { Home } from "./Components/Home";
import { MediaViewer } from "./Components/MediaViewer";

export const AppStart = () => {

  return (
    <Routes>
      <Route path={'/'} element={<Home />} />
      <Route path={'/movie'} element={<MediaViewer />} />
      <Route path={'/series'} element={<MediaViewer />} />
    </Routes>
  );
}