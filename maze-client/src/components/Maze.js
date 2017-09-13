import React from 'react'
import 'whatwg-fetch'

import MazeRow from './MazeRow'
import './Maze.css'

const API_URL = 'http://localhost:49907/api'

export class Maze extends React.Component {
  constructor(props) {
    super(props)
    this.state = {
      loaded: false,
      maze: {},
      path: []
    }
  }

  componentDidMount() {

    window.addEventListener('keydown', this.handleKeyPress)

    const { size } = this.props
    fetch(`${API_URL}/maze/${size}`)
      .then(res => {
        if (res.status !== 200) {
          console.error('Error while retrieving maze from API')
          this.setState({ loaded: false, error: true })
        } else {
          return res.json()
        }
      })
      .then(maze => {
        const path = maze.cells.reduce((prev, next) => [...prev, ...next]).filter(cell => !!cell.start)

        console.log(path)

        this.setState({ maze, path, loaded: true, eventListenerMounted: true })
      })
  }

  componentWillUnmount() {
    if (this.state.eventListenerMounted) {
      window.removeEventListener('keydown', this.handleKeyPress)
    }
  }

  handleKeyPress = e => {
    const value = e.key
    const { maze, path, loaded } = this.state

    if (loaded && ['ArrowLeft', 'ArrowUp', 'ArrowRight', 'ArrowDown'].includes(value)) {
      const current = path[path.length - 1]

      const direction = value.substring(5)

      // Check if invalid move
      if (Maze.isValidMove(maze, current, direction)) {
        console.log('Can move!')
      } else {
        console.log('Can\'t move!')
      }

      e.preventDefault()
    }

  }

  static isValidMove(maze, current, direction) {

    
    
    if ( // If on the boundaries, can't move outside the maze
      (current.row === 0 && direction === 'Up') ||
      (current.col === 0 && direction === 'Left') ||
      (current.row === maze.rows - 1 && direction === 'Down') ||
      (current.col === maze.columns - 1 && direction === 'Right')
    ) {
      return false;
    } else {
      return true
    }
  }

  generateMaze = () => {

    //const { width, height } = this.props
    const { maze, path } = this.state

    const rows = maze.cells.map((row, index) => (<MazeRow cells={row} key={index} />))

    return (
      <table className='maze' onKeyPress={this.onKeyPress} tabIndex='0'>
        <tbody>
          {rows}
        </tbody>
      </table>
    )
  }

  render() {
    let content

    if (this.state.error) {
      content = (<p>Sorry, an error occurred</p>)
    } else if (!this.state.loaded) {
      content = (<p>Please wait, generating maze...</p>)
    } else {
      content = this.generateMaze()
    }
    
    return (
      <div>
        {content}
      </div>
    )
  }
}

export default Maze
