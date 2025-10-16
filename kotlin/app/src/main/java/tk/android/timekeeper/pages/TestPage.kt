package tk.android.timekeeper.pages

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.*
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.colorResource
import tk.android.timekeeper.ui.theme.TimekeeperTheme
import tk.android.timekeeper.NavbarPages
import tk.android.timekeeper.KDataService
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.foundation.Image
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.unit.dp
import androidx.compose.foundation.border
import androidx.compose.material3.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.clickable
import androidx.compose.runtime.*
import androidx.compose.animation.animateColorAsState
import androidx.compose.animation.animateContentSize
import androidx.compose.material.icons.*
import androidx.compose.material.icons.filled.*
import androidx.compose.material.icons.outlined.NotificationAdd
import kotlinx.coroutines.launch
import java.util.Calendar
import java.util.Date

@Composable
fun TestPage(data: KDataService) {

    // Column(modifier = Modifier.padding(3.dp)) {
    Column() {
        Btn(
            name    = "Test Notification",
            onClick = { 
                data.onNotificationButton(null);
            },
        )
        Btn(
            name    = "Test Reminder +10s",
            onClick = { 
                var date = Calendar.getInstance()
                date.add(Calendar.SECOND, 10)

                data.onNotificationButton(getUnixTimestamp(date.getTime()))
            },
        )
    }


    // TODO: get notifications working
}


@Composable
fun Btn(name: String, onClick: () -> Unit) {
        Button(
        onClick = onClick,
    ) {
        Icon(Icons.Outlined.NotificationAdd, "")
        Spacer(modifier = Modifier.padding(1.5.dp))

        Text(name)
    }
}

fun getUnixTimestamp(date: Date): Long {
    return date.getTime() / 1000L
}