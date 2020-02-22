import React from 'react';
import Navbar from 'react-bootstrap/Navbar';
import {CreateVacation} from './vacation/create';

import logo from './logo.svg';
import './App.css';

function App() {
  return (
    <div className="App">
      <Navbar>
        <Navbar.Brand>Vacation Archive</Navbar.Brand>
      </Navbar>
      <CreateVacation/>
    </div>
  );
}

export default App;
