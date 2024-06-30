package com.campus.uclafilemanageservice.domain.service;

import com.campus.uclafilemanageservice.domain.entity.SessionFile;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;
import org.springframework.util.StringUtils;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.util.Objects;

@Service
@RequiredArgsConstructor
public class FileStorageService {

    @Value("${file.upload-dir}")
    private String uploadDir;

    /**
     * Saves a file to the server and returns file information.
     * @param file MultipartFile
     * @param sessionId Session ID
     * @return Metadata of saved files
     */
    public SessionFile storeFile(MultipartFile file, Long sessionId) {
        // Normalize file name
        String originalFileName = StringUtils.cleanPath(Objects.requireNonNull(file.getOriginalFilename()));
        String fileName = System.currentTimeMillis() + "_" + originalFileName;
        try {
            if (fileName.contains("..")) {
                throw new RuntimeException("Filename contains invalid path sequence " + originalFileName);
            }

            Path targetLocation = Paths.get(uploadDir).resolve(fileName);
            Files.createDirectories(targetLocation.getParent());
            Files.copy(file.getInputStream(), targetLocation, StandardCopyOption.REPLACE_EXISTING);

            return new SessionFile(sessionId, fileName, "mp4", targetLocation.toString());
        } catch (IOException ex) {
            throw new RuntimeException("Could not store file " + fileName + ". Please try again!", ex);
        }
    }
}
