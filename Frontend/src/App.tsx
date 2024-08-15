function App() {
  return (
    <div>
      <video width="640" height="480" controls playsInline>
          <source src={`${process.env.REACT_APP_BACKEND_URL}/api/Movie/stream`} type="video/mp4" />
          Az Ön böngészője nem támogatja a videó lejátszást.
      </video>
    </div>
  );
}

export default App;