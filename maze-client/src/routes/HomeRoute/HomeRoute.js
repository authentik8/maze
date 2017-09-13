import React from 'react'
import MazeForm from './MazeForm'
import './HomeRoute.css'

class Home extends React.Component {

  redirectToMaze = (width, height) => {
    window.location.pathname = `/maze/${width}/${height}`
  }

  render() {
    return (
      <div className='Home'>
        <div className='jumbotron'>
          <h3 className='display-5 text-center'>Generate a new maze</h3>
          <div className='card'>
            <div className='card-body'>
              <MazeForm onSubmit={this.redirectToMaze} />
            </div>
          </div>
        </div>
      </div>
    )
  }

}

export default Home
