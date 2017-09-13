import React from 'react'

import './MazeCell.css'

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

    const classes = []

    if (cell.goal) {
      classes.push('goal')
    } else if (cell.start) {
      classes.push('start')
    } else if (this.onPath()) {
      classes.push('onPath')
    }

    const onClick = () => this.props.onClick(cell.row, cell.col)

    return (
      <td
        onClick={onClick}
        className={`cell ${classes.join(' ')}`}
        {...{ style }} />
    )
  }
}

export default MazeCell
