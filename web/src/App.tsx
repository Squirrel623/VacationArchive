import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import {Home} from './home';

import logo from './logo.svg';
import './App.css';

function App() {
  return (
    <div className="App">
      <Navbar>
        <Navbar.Brand>Vacation Archive</Navbar.Brand>
      </Navbar>
      <Home/>
    </div>
  );
}

export default App;
