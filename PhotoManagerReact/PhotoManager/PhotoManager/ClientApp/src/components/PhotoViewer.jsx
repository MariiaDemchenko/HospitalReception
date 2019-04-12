import React, { Component } from 'react';

class PhotoViewer extends Component {

    render() {
        return (renderPhotoView(this.props.selectedPhotos));
    }
}

function renderPhotoView(selectedPhotos) {
    return selectedPhotos.map(p =>
        <div className="photo-viewer">
            <div className="row photo-with-description">
                <table className="table table-bordered table-striped col-md-4">
                    <tbody>
                        <tr>
                            <td>Name</td>
                            <td>{p.photoName}</td>
                        </tr>
                        <tr>
                            <td>Shot date</td>
                            <td>{(new Date(p.shotDate)).toLocaleString()}</td>
                        </tr>
                        <tr>
                            <td>Camera Model</td>
                            <td>{p.cameraModel}</td>
                        </tr>
                        <tr>
                            <td>Lens Focal Length</td>
                            <td>{p.lensFocalLength}</td>
                        </tr>
                    </tbody>
                </table>
                <img src={p.photoUrl} className="col-md-6 photo-frame" alt={p.photoName} />                
            </div>
            <hr/>
        </div>
    );
}

export default PhotoViewer