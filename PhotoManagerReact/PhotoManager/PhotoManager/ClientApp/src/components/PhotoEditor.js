import React, { Component } from 'react';
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { FaCalendarTimes } from 'react-icons/fa';
import ImageUploader from 'react-images-upload';

class PhotoEditor extends Component {
    constructor(props) {
        super(props);
        this.onDrop = this.onDrop.bind(this)
        this.onShotDateChange = this.onShotDateChange.bind(this)
        this.onPhotoNameChange = this.onPhotoNameChange.bind(this)
        this.onCameraChanged = this.onCameraChanged.bind(this)
        this.onLensFocalLengthChanged = this.onLensFocalLengthChanged.bind(this)
        this.onPhotoNameChangeCommon = this.onPhotoNameChange.bind(this)
        this.onRemovePhoto = this.onRemovePhoto.bind(this)
    }

    onDrop = (index) => (pictures, pictureDataURLs) => {
        if (this.props.onDrop)
            this.props.onDrop.call(this, pictures, pictureDataURLs, index);
    }

    onShotDateChange = (index) => (date) => {
        if (this.props.onShotDateChange)
            this.props.onShotDateChange.call(this, index, date);
    }

    onPhotoNameChange = (index) => (event) => {
        if (this.props.onPhotoNameChange)
            this.props.onPhotoNameChange.call(this, index, event);
    }

    onPhotoNameChangeCommon(event) {
        if (this.props.onPhotoNameChange)
            this.props.onPhotoNameChange.call(this, event);
    }

    onCameraChanged = (index) => (event) => {
        if (this.props.onCameraChanged)
            this.props.onCameraChanged.call(this, index, event);
    }

    onLensFocalLengthChanged = (index) => (event) => {
        if (this.props.onLensFocalLengthChanged)
            this.props.onLensFocalLengthChanged.call(this, index, event);
    }

    onRemovePhoto = (index) => (event) => {
        if (this.props.onRemovePhoto)
            this.props.onRemovePhoto.call(this, index);
    }

    renderUploader(includeUploader, onChange, index) {
        if (includeUploader) {
            return (<ImageUploader
                withIcon={true}
                buttonText='Choose images'
                onChange={onChange(index)}
                singleImage={true} />)
        }
    }

    renderImage(photo, currentDate) {
        if (photo.pictureDataURL) {
            return (<img src={photo.pictureDataURL} className="col-md-5 photo-frame" alt={photo.photoName} />)
        }
        return (<img src={`${photo.photoUrl}?v=${currentDate}`} className="col-md-5 photo-frame" alt={photo.photoName}/>)
    }

    render() {
        const currentDate = this.props.currentDate;
        if (this.props.commonDescription) {
            var defaultPhoto = this.props.selectedPhotos[0] || {};
            return (
                <div>
                    <div>
                        <div className="row row info-fields">
                            <label className="col-md-4">Shot date</label>
                            <DatePicker showTimeSelect="true" dateFormat="Pp" selected={defaultPhoto.shotDate} onChange={this.onShotDateChange()} className="form-control col-md-11.2" />
                        </div>
                        <div className="row row info-fields">
                            <label className="col-md-4">Camera Model</label>
                            <input value={defaultPhoto.cameraModel} onChange={this.onCameraChanged()} className="form-control col-md-4" />
                        </div>
                        <div className="row row info-fields">
                            <label className="col-md-4">Lens Focal Length</label>
                            <input value={defaultPhoto.lensFocalLength} onChange={this.onLensFocalLengthChanged()} className="form-control col-md-4" />
                        </div>
                    </div>
                    {this.props.selectedPhotos.map((photo, index) =>
                        <div>
                            <div><a className="btn btn-outline-danger title" ><i onClick={this.onRemovePhoto(index)}><FaCalendarTimes /></i></a></div>
                            <div className="row info-fields">
                                <label className="col-md-4">Name</label>
                                <input value={photo.photoName} onChange={this.onPhotoNameChange(index)} className="form-control col-md-4" />
                            </div>
                            <div className="row">

                                <div className="col-md-8 photo-with-description">
                                    {this.renderImage(photo, currentDate)}
                                </div>
                                <div className="col-md-3">{this.renderUploader(this.props.includeUploader, this.onDrop, index)}</div>
                            </div>

                            <hr />
                        </div>
                    )}
                </div>)
        }
        else {
            return this.props.selectedPhotos.map((photo, index) =>
                <div>
                    <a className="btn btn-outline-danger title" ><i onClick={this.onRemovePhoto(index)}><FaCalendarTimes /></i></a>
                    <div className="row">
                        <div className="col-md-7">
                            <div className="row info-fields">
                                <label className="col-md-4">Name</label>
                                <input value={photo.photoName} onChange={this.onPhotoNameChange(index)} className="form-control col-md-4" />
                            </div>
                            <div className="row row info-fields">
                                <label className="col-md-4">Shot date</label>
                                <DatePicker dateFormat="Pp" showTimeSelect="true" selected={photo.shotDate} onChange={this.onShotDateChange(index)} className="form-control col-md-11.2" />
                            </div>
                            <div className="row row info-fields">
                                <label className="col-md-4">Camera Model</label>
                                <input value={photo.cameraModel} className="form-control col-md-4" onChange={this.onCameraChanged(index)} />
                            </div>
                            <div className="row row info-fields">
                                <label className="col-md-4">Lens Focal Length</label>
                                <input value={photo.lensFocalLength} className="form-control col-md-4" onChange={this.onLensFocalLengthChanged(index)} type="number" />
                            </div>
                        </div>
                        {this.renderImage(photo, currentDate)}
                    </div>
                    {this.renderUploader(this.props.includeUploader, this.onDrop, index)}
                </div>);
        }
    }
}

PhotoEditor.defaultProps = {
    includeUploader: true
};

export default PhotoEditor