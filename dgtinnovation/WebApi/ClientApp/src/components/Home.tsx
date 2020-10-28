import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>Innovation Strategies!</h1>
    <p>Welcome to my test</p>
  </div>
);

export default connect()(Home);
