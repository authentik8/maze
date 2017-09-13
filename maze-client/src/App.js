import React, { Component } from 'react';
import {
  BrowserRouter as Router,
  Route,
  Link
} from 'react-router-dom'
import './App.css';

import Home from './routes/HomeRoute'
import Maze from './routes/MazeRoute'

class App extends Component {
  render() {
    return (
      <Router>
        <div>
          <div className="navbar navbar-expand-lg navbar-dark bg-dark">
            <Link to="/" className="navbar-brand App-mainLink">Maze Solver</Link>
          </div>
          <div className="App-content container">
            <Route exact path="/" component={Home} />
            <Route path="/maze/:size" component={Maze} />
          </div>
        </div>
      </Router>
    );
  }
}

export default App;
