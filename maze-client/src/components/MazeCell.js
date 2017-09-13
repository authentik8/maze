import React from 'react'

export class MazeCell extends React.Component {

  onPath = () => {
    const { cell, path } = this.props
    return path.find(value => value.row === cell.row && value.col === cell.col) !== undefined
  }

  render() {
    const { cell, path } = this.props
    const style = {}

    if (!cell.left) {
      style.borderLeft = 'solid 2px black'
    }
    if (!cell.up) {
      style.borderTop = 'solid 2px black'
    }
    if (!cell.right) {
      style.borderRight = 'solid 2px black'
    }
    if (!cell.down) {
      style.borderBottom = 'solid 2px black'
    }

    if (cell.goal) {
      style.backgroundColor = '#e07264'
    } else if (cell.start) {
      style.backgroundColor = '#72DB75'
      style.borderLeft = '#72DB75'
    } else if (this.onPath()) {
      style.backgroundColor = '#a0c4ff'
    }

    return (
      <td {...{ style }} ></td>
    )
  }
}

export default MazeCell
