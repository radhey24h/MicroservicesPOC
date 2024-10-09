import Particle from "./features/Home/Home";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Dashboard from "./features/Dashboard/Dashboard";

function App() {
  return (
    <>
      <Router>
        <Routes>
          <Route path="/" element={<Particle />} />
          <Route path="/dashboard" element={<Dashboard />} />
          {/* Other routes */}
        </Routes>
      </Router>
    </>
  );
}

export default App;
