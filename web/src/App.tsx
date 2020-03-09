import React from 'react';
import {BrowserRouter as Router, Switch, Route} from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';
import {Home} from './home';
import {List} from './vacation';

import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar>
          <Navbar.Brand>Vacation Archive</Navbar.Brand>
        </Navbar>
        <Switch>
          <Route exact path="/">
            <Home/>
          </Route>
          <Route path="/vacations">
            <List/>
          </Route>
        </Switch>
      </div>
    </Router>
  );
}

export default App;
