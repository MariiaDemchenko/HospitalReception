import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Photo';
import PhotoViewer from './PhotoViewer';

class PhotoView extends Component {
    componentWillUnmount() {
        this.props.onBeforeRedirect();
    }

    render() {
        if (this.props.redirectToEdit) {
            return (<Redirect push to={{ pathname: `/photoEdit`, state: { selectedPhotos: this.props.location.state.selectedPhotos, selectedAlbum: this.props.location.state.selectedAlbum }}} />)
        }
        return (
            <div>
                <PhotoViewer selectedPhotos={this.props.location.state.selectedPhotos} />
            </div>);
    }
}

export default connect(
    state => state.photo,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(PhotoView);