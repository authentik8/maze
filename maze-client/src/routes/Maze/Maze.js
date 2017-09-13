import React from 'react'
import 'whatwg-fetch'

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

    console.log(this.props)
    const { match: { params: { width, height } } } = this.props

    fetch(`${API_URL}/maze/${width}x${height}`)
      .then(res => {
        if (res.status !== 200) {
          console.error('Error retrieving maze from API:')
          console.error(res.text())
          this.setState({ loaded: false, error: true })
        } else {
          return res.json()
        }
      })
      .then(maze => {
        this.setState({ maze, loaded: true })
      })
  }

  render() {

    let content

    if (this.state.error) {
      content = (<p>Sorry, an error occurred.</p>)
    } else if (!this.state.loaded) {
      content = (<p>Please wait, generating maze...</p>)
    } else {
      content = (<p>Maze goes here</p>)
    }

    return (
      <div>
        {content}
      </div>
    )
  }
}

export default Maze
