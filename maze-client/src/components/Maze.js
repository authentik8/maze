import React from 'react'
import 'whatwg-fetch'

import MazeRow from './MazeRow'
import ActionBar from './ActionBar'
import './Maze.css'

const API_URL = 'http://localhost:49907/api'
const MOVEMENTS = ['left', 'up', 'right', 'down']

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

    if (loaded) {
      if (['ArrowLeft', 'ArrowUp', 'ArrowRight', 'ArrowDown'].includes(value)) {
        const direction = value.substring(5).toLowerCase()

        this.handleInput(maze, path, direction)
      } else if (value === 'Backspace') {
        this.undo()
      }
    }
  }

  handleCellClick = (row, col) => {
    const { maze, path } = this.state

    const clickedCell = maze.cells[row][col]

    const pathIndex = path.indexOf(clickedCell)

    if (pathIndex !== -1) {
      this.setState({ path: path.slice(0, pathIndex+1)})
    }
  }

  handleAction = (action) => {

    const { maze, path } = this.state

    if (MOVEMENTS.includes(action)) {
      this.handleInput(maze, path, action)
    } else if (action === 'undo') {
      this.undo()
    }
  }

  handleInput = (maze, path, direction) => {

    const current = path[path.length - 1]

    if (Maze.isValidMove(maze, current, direction)) {

      const next = Maze.move(maze, current, direction)
      const previous = path.length >= 2 ? path[path.length - 2] : false

      let newPath

      // If the previous cell is the same as the "next", then we're retracing our steps and should shorten the path
      if (previous && previous.row === next.row && previous.col === next.col) {
        newPath = path.slice(0, path.length - 1)
      } else {
        newPath = [...path, next]
      }

      this.setState({ path: newPath })
    }
  }

  undo = () => {
    const { path } = this.state
    this.setState({ path: path.slice(0, path.length -1 ) })
  }

  static isValidMove(maze, current, direction) {

    if ( // If on the boundaries, can't move outside the maze
      (current.row === 0 && direction === 'up') ||
      (current.col === 0 && direction === 'left') ||
      (current.row === maze.rows - 1 && direction === 'down') ||
      (current.col === maze.columns - 1 && direction === 'right')
    ) {
      return false;
    } else {
      // `up`, `left`, `down` & `right` values on `Cell` objects indicate whether moves in that direction are valid
      return current[direction]
    }
  }

  static move(maze, current, direction) {
    const movementMap = {
      left: (row, col) => ({ row, col: col - 1 }),
      up: (row, col) => ({ row: row - 1, col }),
      right: (row, col) => ({ row, col: col + 1 }),
      down: (row, col) => ({ row: row + 1, col })
    }

    const { row, col } = movementMap[direction](current.row, current.col)
    return maze.cells[row][col]
  }

  generateMaze = () => {

    const { maze, path } = this.state

    const rows = maze.cells.map((row, index) => (<MazeRow cells={row} key={index} onClick={this.handleCellClick} {...{ path } }/>))
    
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
        <ActionBar handleAction={this.handleAction} />
        <p className='text-center'>
          <small>
            N.B. You can also use the arrow keys to navigate the maze & Backspace to undo!
          </small>
        </p>
      </div>
    )
  }
}

export default Maze
