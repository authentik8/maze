import React from 'react'

import './MazeCell.css'

export class MazeCell extends React.Component {

  shouldComponentUpdate = nextProps => {

    const endOfPath = props => {
      const lastCell = props.path[props.path.length - 1]
      return !!lastCell ? props.cell.row === lastCell.row && props.cell.col === lastCell.col : true
    }

    if (this.props.path.length !== nextProps.path.length) {

      const thisCellEndOfPath = endOfPath(this.props);
      const nextCellEndOfPath = endOfPath(nextProps);

      if (nextProps.path.length > this.props.path.length) {
        return nextCellEndOfPath
      } else {
        return thisCellEndOfPath
      }
    }

    return true
  }

  onPath = path => {
    const { cell } = this.props
    return path.find(value => value.row === cell.row && value.col === cell.col) !== undefined
  }

  render() {
    const { cell, path, solution } = this.props
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
    } else if (this.onPath(path)) {
      classes.push('onPath')
    } else if (solution && this.onPath(solution)) {
      classes.push('onSolution')
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
