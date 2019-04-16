import axios from './AxiosInstance';
import React, { Component } from 'react';
import "react-datepicker/dist/react-datepicker.css";
import { FaSave } from 'react-icons/fa';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { RingLoader } from 'react-spinners';
import ToggleButton from 'react-toggle-button';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Photo';
import NotifierGenerator from './Notification';
import PhotoEditor from './PhotoEditor';

class PhotoEdit extends Component {

    constructor(props) {
        super(props);
        this.props.initItems(this.props.location.state.selectedPhotos, this.props.location.state.selectedAlbum)
    }

    componentWillUnmount() {
        this.props.onBeforeRedirect();
    }

    render() {

        if (this.props.redirectToAlbum) {
            return (<Redirect push to={{ pathname: `/album/${this.props.selectedAlbum.albumId}`, state: { selectedAlbum: this.props.selectedAlbum } }} />)
        }
        if (this.props.postSaveChanges) {
            const onRequestSuccess = this.props.onRequestSuccess
            const onRequestError = this.props.onRequestError
            axios.put(`api/Photos/EditPhotos`, this.props.selectedPhotos)
                .then(function (response) {
                    if (response.status === 200) {
                        onRequestSuccess()
                    }
                })
                .catch(function (error) {
                    if (error.response && error.response.data) {
                        onRequestError(error.response.data)
                    }
                });
        }
        return (

            <div>
                <NotifierGenerator messageText={this.props.alertMessage}
                    messageId={this.props.alertId}
                    messageType={this.props.alertType} />
                <div className='sweet-loading'>
                    <RingLoader
                        color={'#123abc'}
                        loading={this.props.isLoading}
                    />
                </div>
                <div className="row menu"> 
                    <span className="col-md-2 badge badge-info">Set common description </span>
                    <ToggleButton value={this.props.commonDescrition} onToggle={this.props.onCommonDescriptionChanged} className="col-md-4 menu" />
                </div>
                <div><a className="btn btn-outline-primary menu-icon menu" >Save changes <i onClick={this.props.onSaveChanges}><FaSave /></i></a></div>
                <hr />
                <PhotoEditor selectedPhotos={this.props.selectedPhotos}
                    onDrop={this.props.onDrop}
                    currentDate={this.props.currentDate}
                    onShotDateChange={this.props.onShotDateChange}
                    onCameraChanged={this.props.onCameraChanged}
                    onLensFocalLengthChanged={this.props.onLensFocalLengthChanged}
                    commonDescription={this.props.commonDescrition}
                    onPhotoNameChange={this.props.onPhotoNameEditChange}
                    onRemovePhoto={this.props.onRemoveSelectedPhoto}
                />
            </div>);
    }
}

export default connect(
    state => state.photo,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(PhotoEdit);