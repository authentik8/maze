import React from 'react'
import './Home.css'

class Home extends React.Component {

  redirectToMaze = (e) => {
    const form = this.refs.form
    const width = this.refs.width.value
    const height = this.refs.height.value

    if (form.checkValidity() && width && height) {
      window.location.pathname = `/maze/${width}/${height}`
      e.preventDefault()
    }
  }

  render() {
    return (
      <div className='Home'>
        <div className='jumbotron'>
          <h3 className='display-5 text-center'>Generate a new maze</h3>
          <div className='card'>
            <div className='card-body'>
              <form ref='form' onSubmit={this.redirectToMaze} className='form'>

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
            </div>
          </div>
        </div>
      </div>
    )
  }

}

export default Home
