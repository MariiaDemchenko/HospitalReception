import axios from './AxiosInstance';
import React, { Component } from 'react';
import { FaUpload } from 'react-icons/fa';
import ImageUploader from 'react-images-upload';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { RingLoader } from 'react-spinners';
import ToggleButton from 'react-toggle-button';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Photo';
import NotifierGenerator from './Notification';
import PhotoEditor from './PhotoEditor';


class PhotoAdd extends Component {

    constructor(props) {
        super(props);
        this.props.initAlbum(this.props.location.state.selectedAlbum)
    }

    componentWillUnmount() {
        this.props.onBeforeRedirect();
    }

    render() {
        if (this.props.redirectToAlbum) {
            return (<Redirect push to={{ pathname: `/album/${this.props.match.params.albumId}`, state: { selectedAlbum: this.props.selectedAlbum } }} />)
        }
        if (this.props.postSaveChanges) {
            const onRequestSuccess = this.props.onRequestSuccess
            const onRequestError = this.props.onRequestError
            const photos = this.props.selectedPhotos.map(photo => ({ ...photo, albumId: this.props.match.params.albumId }))
            axios.post(`api/Photos/AddPhotos`, photos)
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
                {renderMenu(this.props)}
                {renderUploader(this.props)}
            </div >);
    }
}

function renderUploader(props) {
    return (
        <div>
            <ImageUploader
                withIcon={true}
                buttonText='Choose images'
                onChange={props.onDropToAdd}
                imgExtension={['.jpg', '.gif', '.png', '.gif']}
                maxFileSize={5242880}
            />
            <PhotoEditor selectedPhotos={props.selectedPhotos} includeUploader={false}
                onShotDateChange={props.onShotDateChange}
                onCameraChanged={props.onCameraChanged}
                onLensFocalLengthChanged={props.onLensFocalLengthChanged}
                currentDate={props.currentDate}
                commonDescription={props.commonDescrition}
                onPhotoNameChange={props.onPhotoNameEditChange}
                onRemovePhoto={props.onRemoveSelectedPhoto}
            />
        </div>
    );
}

function renderMenu(props) {
    return (
        <div>
            <div className="row menu">
                <span className="col-md-2 badge badge-info">Set common description </span>
                <ToggleButton value={props.commonDescrition} onToggle={props.onCommonDescriptionChanged} className="col-md-3" />
            </div>
            <div><a className="btn btn-outline-primary menu-icon menu" >Add Photos <i onClick={props.onPhotosUploadClick}><FaUpload /></i></a></div>
            <hr />
        </div>
    )
}

export default connect(
    state => state.photo,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(PhotoAdd);
