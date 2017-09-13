import React from 'react'

export class MazeForm extends React.Component {

  onSubmit = (e) => {
    const form = this.refs.form
    const width = this.refs.width.value
    const height = this.refs.height.value

    if (form.checkValidity() && width && height) {
      this.props.onSubmit(width, height)
      e.preventDefault()
    }
  }

  render() {
    return (
      <form ref='form' onSubmit={this.onSubmit} className='form'>

        <div className='form-group'>
          <label>Width</label>
          <input ref='width' name='width' type='number' className='form-control' required />
        </div>

        <div className='form-group'>
          <label>Height</label>
          <input ref='height' name='height' type='number' className='form-control' />
        </div>

        <input type='submit' value='GO!' onClick={this.redirectToMaze} className='form-control btn-primary' required />
      </form>
    )
  }
}

export default MazeForm
