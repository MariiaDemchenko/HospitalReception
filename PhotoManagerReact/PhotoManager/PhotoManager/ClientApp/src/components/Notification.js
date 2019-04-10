import React from 'react';
import { AlertList } from "react-bs-notifier";

class NotifierGenerator extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            position: "top-right",
            alerts: [],
            timeout: 2000
        };
    }

    generate() {
        const newAlert = {
            type: this.props.messageType,
            message: this.props.messageText

        };

        if (newAlert.message && newAlert.message.length > 0 && (this.state.id !== this.props.messageId)) {
            this.setState({
                alerts: [newAlert],
                messageText: this.props.messageText,
                id: this.props.messageId,
                type: this.props.messageType
            });
        }
    }

    onAlertDismissed(alert) {
        const alerts = this.state.alerts;

        const idx = alerts.indexOf(alert);

        if (idx >= 0) {
            this.setState({
                alerts: [...alerts.slice(0, idx), ...alerts.slice(idx + 1)]
            });
        }
    }

    render() {
        this.generate()
        return (
            <div>
                <AlertList
                    position={this.state.position}
                    alerts={this.state.alerts}
                    timeout={this.state.timeout}
                    onDismiss={this.onAlertDismissed.bind(this)}
                />
            </div>
        );
    }
}

export default NotifierGenerator