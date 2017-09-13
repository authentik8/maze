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
      maze: {}
    }
  }

  componentDidMount() {
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
      .then(maze => this.setState({ maze, loaded: true }))
  }

  generateMaze = () => {

    //const { width, height } = this.props
    const { maze } = this.state

    const rows = maze.cells.map((row, index) => (<MazeRow cells={row} key={index} />))

    console.log(maze.cells)

    return (
      <table className='maze'>
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
