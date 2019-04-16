import React, { Component } from 'react';
import Gallery from 'react-grid-gallery';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Home';

class Home extends Component {

    componentDidMount() {
        this.props.requestDemoPhotos();
    }

    render() {
        return (
            <Gallery images={this.props.images.map(image => ({
                src: `${image.photoUrl}`,
                thumbnail: `${image.thumbUrl}`,
                caption: image.photoName
            } ))}/>) 
    }
}

export default connect(
    state => state.home,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Home);