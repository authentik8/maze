import React from 'react'

class Home extends React.Component {

  render() {
    return (
      <div>
        <h1>Get a new maze</h1>
        <form>
          <label>Width</label>
          <input ref='width' name='width' type='number' />

          <label>Height</label>
          <input ref='height' name='height' type='number' />

          <input type='submit' value='GO!' />
         </form>
      </div>
    )
  }

}

export default Home
